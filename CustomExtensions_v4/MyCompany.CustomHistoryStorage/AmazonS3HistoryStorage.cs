using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Palmmedia.ReportGenerator.Core.Reporting.History;

namespace MyCompany.CustomHistoryStorage
{
    public class AmazonS3HistoryStorage : IHistoryStorage
    {
        private readonly string MyBucketName = "*** Provide bucket name ***";

        private readonly string AwsAccessKeyId = "*** Provide AwsAccessKeyId ***";

        private readonly string AwsSecretAccessKey = "*** Provide AwsSecretAccessKey ***";

        public AmazonS3HistoryStorage()
        {
            Dictionary<string, string> arguments = this.GetCommandLineArgumentsByKey();

            string value = null;

            if (arguments.TryGetValue("BUCKETNAME", out value))
            {
                this.MyBucketName = value;
            }

            if (arguments.TryGetValue("AWSACCESSKEYID", out value))
            {
                this.AwsAccessKeyId = value;
            }

            if (arguments.TryGetValue("AWSSECRETACCESSKEY", out value))
            {
                this.AwsSecretAccessKey = value;
            }
        }

        public IEnumerable<string> GetHistoryFilePaths()
        {
            var credentials = new BasicAWSCredentials(AwsAccessKeyId, AwsSecretAccessKey);
            using (var client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1))
            {
                ListObjectsV2Request request = new ListObjectsV2Request
                {
                    BucketName = MyBucketName,
                    MaxKeys = 200
                };

                ListObjectsV2Response response;
                do
                {
                    response = client.ListObjectsV2Async(request).Result;

                    // Process response.
                    foreach (S3Object entry in response.S3Objects)
                    {
                        yield return entry.Key;
                    }

                    request.ContinuationToken = response.NextContinuationToken;
                }
                while (response.IsTruncated == true);
            }
        }

        public Stream LoadFile(string filePath)
        {
            var credentials = new BasicAWSCredentials(AwsAccessKeyId, AwsSecretAccessKey);
            using (var client = new AmazonS3Client(AwsAccessKeyId, AwsSecretAccessKey, Amazon.RegionEndpoint.USEast1))
            {
                TransferUtility fileTransferUtility = new TransferUtility(client);

                return fileTransferUtility.OpenStream(MyBucketName, filePath);
            }
        }

        public void SaveFile(Stream stream, string fileName)
        {
            var credentials = new BasicAWSCredentials(AwsAccessKeyId, AwsSecretAccessKey);
            using (var client = new AmazonS3Client(AwsAccessKeyId, AwsSecretAccessKey, Amazon.RegionEndpoint.USEast1))
            {
                TransferUtility fileTransferUtility = new TransferUtility(client);

                fileTransferUtility.Upload(stream, MyBucketName, fileName);
            }
        }

        private Dictionary<string, string> GetCommandLineArgumentsByKey()
        {
            var namedArguments = new Dictionary<string, string>();

            foreach (var arg in Environment.GetCommandLineArgs())
            {
                var match = Regex.Match(arg, "-(?<key>\\w{2,}):(?<value>.+)");

                if (match.Success)
                {
                    namedArguments[match.Groups["key"].Value.ToUpperInvariant()] = match.Groups["value"].Value;
                }
            }

            return namedArguments;
        }
    }
}
