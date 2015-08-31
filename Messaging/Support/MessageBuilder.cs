using System;
using System.Collections.Generic;

namespace Cloudmark.Messaging.Support {
    public class MessageBuilder<T> :
    {
        public override Message<T> build()
        {
            throw new NotImplementedException();
        }

        public override AbstractIntegrationMessageBuilder<T> CopyHeaders(IDictionary<string, object> headersToCopy)
        {
            throw new NotImplementedException();
        }

        public override AbstractIntegrationMessageBuilder<T> CopyHeadersIfAbsent(IDictionary<string, object> headersToCopy)
        {
            throw new NotImplementedException();
        }

        public override AbstractIntegrationMessageBuilder<T> RemoveHeader(string headerName)
        {
            throw new NotImplementedException();
        }

        public override AbstractIntegrationMessageBuilder<T> RemoveHeaders(params string[] headerPatterns)
        {
            throw new NotImplementedException();
        }

        public override AbstractIntegrationMessageBuilder<T> SetHeader(string headerName, object headerValue)
        {
            throw new NotImplementedException();
        }

        public override AbstractIntegrationMessageBuilder<T> SetHeaderIfAbsent(string headerName, object headerValue)
        {
            throw new NotImplementedException();
        }

        protected override object GetCorrelationId()
        {
            throw new NotImplementedException();
        }

        protected override List<List<object>> GetSequenceDetails()
        {
            throw new NotImplementedException();
        }

        protected override object GetSequenceNumber()
        {
            throw new NotImplementedException();
        }

        protected override object GetSequenceSize()
        {
            throw new NotImplementedException();
        }
    }
}