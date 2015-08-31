using System;

namespace Cloudmark.Messaging.Exception {
	public class MessageDeliveryException<T>: Exception {
		public MessageDeliveryException(string description): base(description) {
		}
		
		public MessageDeliveryException(Message<T> undeliveredMessage): base(undeliveredMessage.ToString()) {
			
		}
		
		
	}	
}