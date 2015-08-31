using System;

namespace Cloudmark.Messaging {
	
	public interface SubscribableChannel: MessageChannel {
		bool Subscribe(MessageHandler action);
		bool UnSubscribe(MessageHandler action);	
	}
}