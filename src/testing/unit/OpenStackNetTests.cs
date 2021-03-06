﻿using System;
using System.Net.Http.Headers;
using Flurl.Http;
using OpenStack.Testing;
using Xunit;

namespace OpenStack
{
    public class OpenStackNetTests : IDisposable
    {
        public void Dispose()
        {
            OpenStackNet.Configuration.ResetDefaults();
        }

        [Fact]
        public async void UserAgentTest()
        {
            using (var httpTest = new HttpTest())
            {
                OpenStackNet.Configure();

                await "http://api.com".GetAsync();

                var userAgent = httpTest.CallLog[0].Request.Headers.UserAgent.ToString();
                Assert.Contains("openstack.net", userAgent);
            }
        }

        [Fact]
        public async void UserAgentWithApplicationSuffixTest()
        {
            using (var httpTest = new HttpTest())
            {
                OpenStackNet.Configure(configure: options => options.UserAgents.Add(new ProductInfoHeaderValue("(unittests)")));
                
                await "http://api.com".GetAsync();

                var userAgent = httpTest.CallLog[0].Request.Headers.UserAgent.ToString();
                Assert.Contains("openstack.net", userAgent);
                Assert.Contains("unittests", userAgent);
            }
        }
    }
}
