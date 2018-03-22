using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Rest;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FaceIdentification.CognitiveServices
{
    public class FaceRecognizer : IFaceRecognizer
    {
        private IFaceAPI faceAPI;
        private string personGroupId;

        public FaceRecognizer(string subscriptionKey, AzureRegions region, string personGroupId)
        {
            this.faceAPI = new FaceAPI(new ApiKeyServiceClientCredentials(subscriptionKey));
            this.faceAPI.AzureRegion = region;

            this.personGroupId = personGroupId;
        }

        public async Task<bool> IsGroupExistingAsync()
        {
            try
            {
                var result = await this.faceAPI.PersonGroup.GetWithHttpMessagesAsync(this.personGroupId);

                return result.Response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task CreateGroupAsync()
        {
            var result = await this.faceAPI.PersonGroup.CreateWithHttpMessagesAsync(this.personGroupId, this.personGroupId);
        }

        public async Task<IEnumerable<Face>> RecognizeImageAsync(byte[] image)
        {
            var result = new List<Face>();

            using (var stream = new MemoryStream(image))
            {
                var faces = await this.faceAPI.Face.DetectWithStreamAsync(stream);

                if (faces.Any())
                {
                    var faceIds = faces.Select(f => f.FaceId.Value).ToList();
                    var identities = await this.faceAPI.Face.IdentifyAsync(personGroupId, faceIds);

                    foreach (var identity in identities)
                    {
                        var face = new Face()
                        {
                            FaceId = identity.FaceId,
                            Candidates = new List<Person>()
                        };

                        foreach (var candidate in identity.Candidates)
                        {
                            var person = await this.faceAPI.PersonGroupPerson.GetAsync(personGroupId, candidate.PersonId);

                            face.Candidates.Add(new Person()
                            {
                                PersonId = person.PersonId,
                                Name = person.Name,
                                UserData = person.UserData,
                                Confidence = candidate.Confidence
                            });
                        }

                        result.Add(face);
                    }
                }
            }

            return result;
        }

        public async Task AddImageToExistingPersonAsync(Guid personId, byte[] image, string userData = null)
        {
            using (var stream = new MemoryStream(image))
            {
                var face = await this.faceAPI.PersonGroupPerson.AddPersonFaceFromStreamAsync(this.personGroupId, personId, stream, userData);
            }

            await this.faceAPI.PersonGroup.TrainAsync(this.personGroupId);
        }

        public async Task<Guid> AddImageAsync(byte[] image, string name, string userData = null)
        {
            var person = await this.faceAPI.PersonGroupPerson.CreateAsync(this.personGroupId, name, userData);

            await AddImageToExistingPersonAsync(person.PersonId, image, userData);

            return person.PersonId;
        }
    }
}
