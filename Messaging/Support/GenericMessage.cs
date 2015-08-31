using System.Collections.Generic;

namespace Cloudmark.Messaging.Support {

    public class GenericMessage<T> : Message<T>
    {
		private readonly T payload;
		private readonly MessageHeaders headers; 
		
		public GenericMessage(T payload ): this(payload, new MessageHeaders(null)){
		}
		
		public GenericMessage(T payload,  IDictionary<string, object> header): this(payload, new MessageHeaders(header)){
        }
        
        public GenericMessage(T payload, MessageHeaders headers){
            if (payload == null) throw null;
            if (headers == null) throw null; 
            this.payload = payload; 
            this.headers = headers; 
        }
		
        public MessageHeaders getHeaders()
        {
            return this.headers; 
        }

        public T getPayload()
        {
            return this.payload;
        }
        
        public override string ToString(){
            return this.GetType().FullName +  "[payload=" + this.payload + "] " +  "[headers=]" + this.headers + "]"; 
        }
    }

}