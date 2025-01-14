// Copyright 2020 New Relic, Inc. All rights reserved.
// SPDX-License-Identifier: Apache-2.0


using NewRelic.Agent.IntegrationTestHelpers;

namespace NewRelic.Agent.IntegrationTests.RemoteServiceFixtures.AwsLambda
{
    public abstract class LambdaAPIGatewayProxyRequestTriggerFixtureBase : LambdaSelfExecutingAssemblyFixture
    {
        private static string GetHandlerString(bool isAsync, bool returnsStream)
        {
            return "LambdaSelfExecutingAssembly::LambdaSelfExecutingAssembly.Program::ApiGatewayProxyRequestHandler" + (returnsStream ? "ReturnsStream" : "") + (isAsync ? "Async" : "");
        }

        protected LambdaAPIGatewayProxyRequestTriggerFixtureBase(string targetFramework, bool isAsync, bool returnsStream) :
            base(targetFramework,
                null,
                GetHandlerString(isAsync, returnsStream),
                "ApiGatewayProxyRequestHandler" + (returnsStream ? "ReturnsStream" : "") + (isAsync ? "Async" : ""),
                null)
        {
        }

        public void EnqueueAPIGatewayProxyRequest()
        {
            var apiGatewayProxyRequestJson = $$"""
                                               {
                                                 "body": "{\"test\":\"body\"}",
                                                 "resource": "/{proxy+}",
                                                 "path": "/path/to/resource",
                                                 "httpMethod": "POST",
                                                 "queryStringParameters": {
                                                   "foo": "bar"
                                                 },
                                                 "pathParameters": {
                                                   "proxy": "path/to/resource"
                                                 },
                                                 "stageVariables": {
                                                   "baz": "qux"
                                                 },
                                                 "headers": {
                                                   "Accept": "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                                                   "Accept-Encoding": "gzip, deflate, sdch",
                                                   "Accept-Language": "en-US,en;q=0.8",
                                                   "Cache-Control": "max-age=0",
                                                   "CloudFront-Forwarded-Proto": "https",
                                                   "CloudFront-Is-Desktop-Viewer": "true",
                                                   "CloudFront-Is-Mobile-Viewer": "false",
                                                   "CloudFront-Is-SmartTV-Viewer": "false",
                                                   "CloudFront-Is-Tablet-Viewer": "false",
                                                   "CloudFront-Viewer-Country": "US",
                                                   "Host": "1234567890.execute-api.{dns_suffix}",
                                                   "Upgrade-Insecure-Requests": "1",
                                                   "User-Agent": "Custom User Agent String",
                                                   "Via": "1.1 08f323deadbeefa7af34d5feb414ce27.cloudfront.net (CloudFront)",
                                                   "X-Amz-Cf-Id": "cDehVQoZnx43VYQb9j2-nvCh-9z396Uhbp027Y2JvkCPNLmGJHqlaA==",
                                                   "X-Forwarded-For": "127.0.0.1, 127.0.0.2",
                                                   "X-Forwarded-Port": "443",
                                                   "X-Forwarded-Proto": "https"
                                               },
                                                 "requestContext": {
                                                   "accountId": "123456789012",
                                                   "resourceId": "123456",
                                                   "stage": "prod",
                                                   "requestId": "c6af9ac6-7b61-11e6-9a41-93e8deadbeef",
                                                   "identity": {
                                                     "cognitoIdentityPoolId": null,
                                                     "accountId": null,
                                                     "cognitoIdentityId": null,
                                                     "caller": null,
                                                     "apiKey": null,
                                                     "sourceIp": "127.0.0.1",
                                                     "cognitoAuthenticationType": null,
                                                     "cognitoAuthenticationProvider": null,
                                                     "userArn": null,
                                                     "userAgent": "Custom User Agent String",
                                                     "user": null
                                                   },
                                                   "resourcePath": "/{proxy+}",
                                                   "httpMethod": "POST",
                                                   "apiId": "1234567890"
                                                 }
                                               }
                                               """;
            EnqueueLambdaEvent(apiGatewayProxyRequestJson);
        }

        public void EnqueueAPIGatewayProxyRequestWithDTHeaders(string traceId, string spanId)
        {
            var apiGatewayProxyRequestJson = $$"""
                                               {
                                                 "body": "{\"test\":\"body\"}",
                                                 "resource": "/{proxy+}",
                                                 "path": "/path/to/resource",
                                                 "httpMethod": "POST",
                                                 "queryStringParameters": {
                                                   "foo": "bar"
                                                 },
                                                 "pathParameters": {
                                                   "proxy": "path/to/resource"
                                                 },
                                                 "stageVariables": {
                                                   "baz": "qux"
                                                 },
                                                 "headers": {
                                                   "Accept": "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8",
                                                   "Accept-Encoding": "gzip, deflate, sdch",
                                                   "Accept-Language": "en-US,en;q=0.8",
                                                   "Cache-Control": "max-age=0",
                                                   "CloudFront-Forwarded-Proto": "https",
                                                   "CloudFront-Is-Desktop-Viewer": "true",
                                                   "CloudFront-Is-Mobile-Viewer": "false",
                                                   "CloudFront-Is-SmartTV-Viewer": "false",
                                                   "CloudFront-Is-Tablet-Viewer": "false",
                                                   "CloudFront-Viewer-Country": "US",
                                                   "Host": "1234567890.execute-api.{dns_suffix}",
                                                   "Upgrade-Insecure-Requests": "1",
                                                   "User-Agent": "Custom User Agent String",
                                                   "Via": "1.1 08f323deadbeefa7af34d5feb414ce27.cloudfront.net (CloudFront)",
                                                   "X-Amz-Cf-Id": "cDehVQoZnx43VYQb9j2-nvCh-9z396Uhbp027Y2JvkCPNLmGJHqlaA==",
                                                   "X-Forwarded-For": "127.0.0.1, 127.0.0.2",
                                                   "X-Forwarded-Port": "443",
                                                   "X-Forwarded-Proto": "https",
                                                   "traceparent": "{{GetTestTraceParentHeaderValue(traceId, spanId)}}",
                                                   "tracestate": "{{GetTestTraceStateHeaderValue(spanId)}}"
                                                 },
                                                 "requestContext": {
                                                   "accountId": "123456789012",
                                                   "resourceId": "123456",
                                                   "stage": "prod",
                                                   "requestId": "c6af9ac6-7b61-11e6-9a41-93e8deadbeef",
                                                   "identity": {
                                                     "cognitoIdentityPoolId": null,
                                                     "accountId": null,
                                                     "cognitoIdentityId": null,
                                                     "caller": null,
                                                     "apiKey": null,
                                                     "sourceIp": "127.0.0.1",
                                                     "cognitoAuthenticationType": null,
                                                     "cognitoAuthenticationProvider": null,
                                                     "userArn": null,
                                                     "userAgent": "Custom User Agent String",
                                                     "user": null
                                                   },
                                                   "resourcePath": "/{proxy+}",
                                                   "httpMethod": "POST",
                                                   "apiId": "1234567890"
                                                 }
                                               }
                                               """;
            EnqueueLambdaEvent(apiGatewayProxyRequestJson);
        }

        /// <summary>
        /// A minimal payload to validate the fix for https://github.com/newrelic/newrelic-dotnet-agent/issues/2528
        /// </summary>
        public void EnqueueMinimalAPIGatewayProxyRequest()
        {
            var apiGatewayProxyRequestJson = $$"""
                                               {
                                                 "body": "{\"test\":\"body\"}",
                                                 "path": "/path/to/resource",
                                                 "httpMethod": "POST"
                                               }
                                               """;
            EnqueueLambdaEvent(apiGatewayProxyRequestJson);
        }

        /// <summary>
        /// An invalid payload to validate the fix for https://github.com/newrelic/newrelic-dotnet-agent/issues/2652
        /// </summary>
        public void EnqueueInvalidAPIGatewayProxyRequest()
        {
            var invalidApiGatewayProxyRequestJson = $$"""
                                                     {
                                                       "foo": "bar"
                                                     }
                                                     """;
            EnqueueLambdaEvent(invalidApiGatewayProxyRequestJson);
        }
    }

    public class LambdaAPIGatewayProxyRequestTriggerFixtureCoreOldest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public LambdaAPIGatewayProxyRequestTriggerFixtureCoreOldest() : base(CoreOldestTFM, false, false) { }
    }

    public class AsyncLambdaAPIGatewayProxyRequestTriggerFixtureCoreOldest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public AsyncLambdaAPIGatewayProxyRequestTriggerFixtureCoreOldest() : base(CoreOldestTFM, true, false) { }
    }

    public class LambdaAPIGatewayProxyRequestTriggerFixtureCoreLatest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public LambdaAPIGatewayProxyRequestTriggerFixtureCoreLatest() : base(CoreLatestTFM, false, false) { }
    }

    public class AsyncLambdaAPIGatewayProxyRequestTriggerFixtureCoreLatest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public AsyncLambdaAPIGatewayProxyRequestTriggerFixtureCoreLatest() : base(CoreLatestTFM, true, false) { }
    }

    public class LambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreOldest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public LambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreOldest() : base(CoreOldestTFM, false, true) { }
    }

    public class AsyncLambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreOldest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public AsyncLambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreOldest() : base(CoreOldestTFM, true, true) { }
    }

    public class LambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreLatest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public LambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreLatest() : base(CoreLatestTFM, false, true) { }
    }

    public class AsyncLambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreLatest : LambdaAPIGatewayProxyRequestTriggerFixtureBase
    {
        public AsyncLambdaAPIGatewayProxyRequestReturnsStreamTriggerFixtureCoreLatest() : base(CoreLatestTFM, true, true) { }
    }
}
