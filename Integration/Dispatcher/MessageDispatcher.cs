using Cloudmark.Messaging;

namespace Cloudmark.Integration.Dispatcher {
	public interface MessageDispatcher {
		bool AddHandler(Messaging.MessageHandler handler); 
	}
	
}