using System;
using Cloudmark.Messaging;

namespace Cloudmark.Messaging.Support {
	/// Interface for interceptors that are able to view and/or modify the Message 
	/// being sent-to and/or received-from a MessageChannel
	public interface ChannelInterceptor {
		/// Invoked before the Message is actually sent to the channel. 
		/// This allows for motification of the Message if necessary. 
		/// If this mehtod returns (@code null) then the actual invokation will not occur.  
		Message PreSend(Message message, MessageChannel channel);
		/// Invoked immediately after the send invocation.  The boolean value 
		/// argument represents the return value of that invocation.  
		void PostSend(Message message, MessageChannel channel, bool sent);
		/// Invoked after the completion of a send regardless of any exceptions that have 
		/// been raised thus allowing for proper resource cleanup. 
		/// Note that this will only be invoked if the #preSend successfully completed and return a Message, 
		/// i.e. it did not return null.  
		bool PreReceive(MessageChannel channel);
		/// Invoked immediately after a Message has been retrieved but before it is returned to the caller.  The 
		/// Message may be modified if neccessary.  The only applies to PollableChannels. 
		Message PostRecieve(Message message, MessageChannel channel);
	}
	
}