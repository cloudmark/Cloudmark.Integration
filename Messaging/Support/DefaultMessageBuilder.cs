using System;
using System.Collections.Generic;

/// The default message builder; creates immutable GenericMessage(s).  
namespace Cloudmark.Messaging.Support {

    public sealed class DefaultMessageBuilder<T> : AbstractIntegrationMessageBuilder<T>
    {
		private readonly T Payload;

		private readonly IntegrationMessageHeaderAccessor HeaderAccessor;
	
		private readonly Message<T> OriginalMessage;
	
		private volatile bool Modified;


        public override Message<T> Build()
        {
			if (!this.Modified && !this.HeaderAccessor.isModified() && this.OriginalMessage != null) {
				return this.originalMessage;
			}
			if (this.payload instanceof Throwable) {
				return (Message<T>) new ErrorMessage((Throwable) this.payload, this.headerAccessor.toMap());
			}
			return new GenericMessage<T>(this.payload, this.headerAccessor.toMap());
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