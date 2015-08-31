using System;
using Cloudmark.Messaging;
using System.Collections.Generic;
using Cloudmark.Messaging.Support;
using Cloudmark.Messaging.Converter;

namespace Cloudmark.Integration.Channel
{

    public abstract class AbstractMessageChannel : MessageChannel
    {
        public virtual string ComponentType => "channel";

        public virtual string ChannelName { get; set; }

        public virtual bool ShouldTrack { get; set; }
        public virtual List<Type> DataTypes { get; set; }

        private volatile bool loggingEnabled = true;

        private volatile MessageConverter messageConverter;

        public virtual List<ChannelInterceptor> ChannelInterceptor { get; set; }


        public void AddInterceptor(ChannelInterceptor interceptor)
        {
            this.ChannelInterceptor.Add(interceptor);
        }

        public void AddInterceptor(int index, ChannelInterceptor interceptor)
        {
            this.ChannelInterceptor.Insert(index, interceptor);
        }

        public void RemoveInterceptor(int index)
        {
            this.ChannelInterceptor.RemoveAt(index);
        }

        public void RemoveInterceptor(ChannelInterceptor interceptor)
        {
            this.ChannelInterceptor.Remove(interceptor);
        }


        private Message<T> ConvertPayloadIfNecessary<T>(Message message)
        {
            // first pass checks if the payload type already matches any of the datatypes
            foreach (Type datatype in this.DataTypes)
            {
                // if (datatype.IsAssignableFrom(message.getPayload().GetType())) {
                //    return (Message<T>)message;
                //}
            }
            if (this.messageConverter != null)
            {
                throw new NotImplementedException("This functionality is not yet implemented");
            }
            return null;
            // TODO: CREATE THE MESSAGE DELIVERY EXCEPTION.  
            //  throw new MessageDeliveryException(message, "Channel '" + this.getComponentName() +
            //          "' expected one of the following datataypes [" +
            //          StringUtils.arrayToCommaDelimitedString(this.datatypes) +
            //          "], but received [" + message.getPayload().getClass() + "]");
        }

        public bool Send(Message msg)
        {
            return this.Send(msg, -1);
        }

        public bool Send(Message msg, long timeout)
        {
            bool sent = false; 
            if (msg == null) throw null;
            if (this.DataTypes.Count > 0)
            {
                Message<object> message = this.ConvertPayloadIfNecessary<object>(msg);
            }
            bool debugEnabled = this.loggingEnabled;
            if (debugEnabled)
            {
                System.Console.WriteLine("PreSend on Channel " + this + ", message: " + msg);
            }

            if (this.ChannelInterceptor?.Exists(ci => ci.PreSend(msg, this) == null) ?? false) return false; 
            
            sent = this.doSend(msg, timeout);
            
            
			if (debugEnabled) {
				System.Console.WriteLine("OostSend (sent=" + sent + ") on channel '" + this + "', message: " + msg);
			}
            
            this.ChannelInterceptor.ForEach(ci => ci.PostSend(msg, this, sent));
            return sent; 
        }
        
        protected abstract bool doSend(Message message, long timeout);
    }

}