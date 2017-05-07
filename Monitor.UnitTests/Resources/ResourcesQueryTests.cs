using System;
using System.Collections.Generic;
using Monitor.Modules.Resources;
using Monitor.Modules.Resources.Query;
using NUnit.Framework;
using Resource = Monitor.Modules.Resources.Query.Resource;

namespace Monitor.UnitTests.Resources
{
    public interface IDbAdapter
    {
        dynamic SimpleData { get;}
    }

[TestFixture]
    public class ResourcesQueryTests
    {
        private static readonly Guid ResourceGuid = Guid.Parse("5176276f-081d-4e4c-82a9-e80b1ec40d4d");
        private static readonly Guid SensorGuid = Guid.Parse("8a2a0fc0-0067-48be-8c8a-a16d6d643b84");
        private static readonly Guid Sensor2Guid = Guid.Parse("e011e901-0431-425e-a8c2-3d22651c5b96");

        private static readonly Guid Resource2Guid = Guid.Parse("e3d80ee5-ecf1-4fe1-a261-22c3d1d6f9cf");
        private static readonly Guid Sensor3Guid = Guid.Parse("27d256eb-4377-4e2c-9818-510305e32ddb");
        private static readonly Guid Sensor4Guid = Guid.Parse("dd104bda-65bb-479c-ac21-8518db3f8f97");

        private static readonly string Resource1Name = "resource1";
        private static readonly string Description1 = "description1";
        private static readonly string Resource2Name = "resource2";
        private static readonly string Description2 = "description2";

        private static readonly string ApiBasePath = "http://127.0.0.1:8000";

        private void InsertData(IDbAdapter db)
        {
            InsertResource(db, Resource1Name, Description1, ResourceGuid, SensorGuid, Sensor2Guid);
            InsertResource(db, Resource2Name, Description2, Resource2Guid, Sensor3Guid, Sensor4Guid);
        }

        private static void InsertResource(IDbAdapter db, object name, string description, Guid resourceGuid,
            Guid sensor1Guid, Guid sensor2Guid)
        {
            db.SimpleData.Resource.Insert(name: name, description: description, guid: resourceGuid.ToString());
            long resourceId = db.SimpleData.Resource.FindByGuid(resourceGuid.ToString()).Id;
            db.SimpleData.Sensor.Insert(guid: sensor1Guid.ToString(), metric: "CPU", unit: "%", complex: 0,
                resourceId: resourceId);
            db.SimpleData.Sensor.Insert(guid: sensor2Guid.ToString(), metric: "Memory usage", unit: "MB", complex: 0,
                resourceId: resourceId);
        }

        private static ResourcesQuery CreateResourcesQuery(IDbAdapter db)
        {
            return null;
//            return new ResourcesQuery(db);
        }

        private static IEnumerable<TestCaseData> TestCases()
        {
            yield return new TestCaseData(new ResourcesQueryParameters
            {
                Name = "Something"
            }, new ResourcesResponse
            {
                Page = new PageDetails {Number = 1, Size = 100, TotalCount = 0},
                Resources = new Resource[0]
            }).SetName("Empty result");
            var resource1 = new Resource
            {
                Id = ResourceGuid,
                Name = Resource1Name,
                Description = Description1,
                Measurements = new[]
                {
                    $"{ApiBasePath}/measurements/{SensorGuid}",
                    $"{ApiBasePath}/measurements/{Sensor2Guid}"
                }
            };
            var resource2 = new Resource
            {
                Id = Resource2Guid,
                Name = Resource2Name,
                Description = Description2,
                Measurements = new[]
                {
                    $"{ApiBasePath}/measurements/{Sensor3Guid}",
                    $"{ApiBasePath}/measurements/{Sensor4Guid}"
                }
            };
            yield return new TestCaseData(new ResourcesQueryParameters(), new ResourcesResponse
            {
                Page = new PageDetails {Number = 1, Size = 100, TotalCount = 2},
                Resources = new[]
                {
                    resource1,
                    resource2
                }
            }).SetName("All results");
            yield return new TestCaseData(new ResourcesQueryParameters {Name = Resource1Name}, new ResourcesResponse
            {
                Page = new PageDetails {Number = 1, Size = 100, TotalCount = 1},
                Resources = new[]
                {
                    resource1
                }
            }).SetName("Single result, full name");
            yield return new TestCaseData(new ResourcesQueryParameters {Name = "source1"}, new ResourcesResponse
            {
                Page = new PageDetails {Number = 1, Size = 100, TotalCount = 1},
                Resources = new[]
                {
                    resource1
                }
            }).SetName("Single result, partial name");

            yield return new TestCaseData(new ResourcesQueryParameters {Page = 1, PageSize = 1}, new ResourcesResponse
            {
                Page = new PageDetails {Number = 1, Size = 1, TotalCount = 2},
                Resources = new[]
                {
                    resource1
                }
            }).SetName("First page");

            yield return new TestCaseData(new ResourcesQueryParameters {Page = 2, PageSize = 1}, new ResourcesResponse
            {
                Page = new PageDetails {Number = 2, Size = 1, TotalCount = 2},
                Resources = new[]
                {
                    resource2
                }
            }).SetName("Second page");
        }


        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void Test(ResourcesQueryParameters parameters, ResourcesResponse expectedResult)
        {
            using (var db = new TestDatabase())
            {
                InsertData(db.Db);
                var resourcesQuery = CreateResourcesQuery(db.Db);

                var result = resourcesQuery.Get(parameters);

                Assert.That(result, Is.EqualTo(expectedResult));
            }
        }
    }
}