using System;
using Cloudmark.Messaging;
using System.Collections.Generic;
using Cloudmark.Messaging.Support;
using Cloudmark.Messaging.Converter;
using Cloudmark.Messaging;

namespace Cloudmark.Integration.Channel {


    public abstract class AbstractSubscribableChannel : AbstractMessageChannel, SubscribableChannel
    {
		 
		
        public bool Subscribe(Action<Message> action)
        {
            throw new NotImplementedException();
        }

        public bool UnSubscribe(Action<Message> action)
        {
            throw new NotImplementedException();
        }
		
		protected abstract MessageDispatcher GetDispatcher(); 
    }

}