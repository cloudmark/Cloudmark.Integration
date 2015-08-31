namespace Cloudmark.Messaging.Converter {
	public interface MessageConverter {
		Message<T> fromMessage<S, T>(Message<S> message, T targetType);
		
		Message<T> toMessage<T>(object obj, MessageHeaders headers);
	}
}