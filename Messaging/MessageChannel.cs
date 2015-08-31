namespace Cloudmark.Messaging {
	public interface MessageChannel {
		bool Send(Message msg);
	
		bool Send(Message msg, long timeout);
	}
}