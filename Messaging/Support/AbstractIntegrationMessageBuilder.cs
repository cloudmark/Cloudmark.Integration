using System;
using System.Collections.Generic;

namespace Cloudmark.Messaging.Support {
	
	public abstract class AbstractIntegrationMessageBuilder<T> {
		/**
	 * Set the value for the given header name. If the provided value is <code>null</code>, the header will be removed.
	 *
	 * @param headerName The header name.
	 * @param headerValue The header value.
	 * @return this.
	 */
	public abstract AbstractIntegrationMessageBuilder<T> SetHeader(String headerName, Object headerValue);

	/**
	 * Set the value for the given header name only if the header name is not already associated with a value.
	 *
	 * @param headerName The header name.
	 * @param headerValue The header value.
	 * @return this.
	 */
	public abstract AbstractIntegrationMessageBuilder<T> SetHeaderIfAbsent(String headerName, Object headerValue);

	/**
	 * Removes all headers provided via array of 'headerPatterns'. As the name suggests the array
	 * may contain simple matching patterns for header names. Supported pattern styles are:
	 * "xxx*", "*xxx", "*xxx*" and "xxx*yyy".
	 *
	 * @param headerPatterns The header patterns.
	 * @return this.
	 */
	public abstract AbstractIntegrationMessageBuilder<T> RemoveHeaders(params string[] headerPatterns);

	/**
	 * Remove the value for the given header name.
	 * @param headerName The header name.
	 * @return this.
	 */
	public abstract AbstractIntegrationMessageBuilder<T> RemoveHeader(String headerName);

	/**
	 * Copy the name-value pairs from the provided Map. This operation will overwrite any existing values. Use {
	 * {@link #copyHeadersIfAbsent(Map)} to avoid overwriting values. Note that the 'id' and 'timestamp' header values
	 * will never be overwritten.
	 *
	 * @param headersToCopy The headers to copy.
	 * @return this.
	 *
	 * @see MessageHeaders#ID
	 * @see MessageHeaders#TIMESTAMP
	 */
	public abstract AbstractIntegrationMessageBuilder<T> CopyHeaders(IDictionary<string, object> headersToCopy);

	/**
	 * Copy the name-value pairs from the provided Map. This operation will <em>not</em> overwrite any existing values.
	 *
	 * @param headersToCopy The headers to copy.
	 * @return this.
	 */
	public abstract AbstractIntegrationMessageBuilder<T> CopyHeadersIfAbsent(IDictionary<String, object> headersToCopy);

	public AbstractIntegrationMessageBuilder<T> SetExpirationDate(long expirationDate) {
		return this.SetHeader(IntegrationMessageHeaderAccessor.EXPIRATION_DATE, expirationDate);
	}

	public AbstractIntegrationMessageBuilder<T> SetExpirationDate(DateTime expirationDate) {
		if (expirationDate != null) {
			return this.SetHeader(IntegrationMessageHeaderAccessor.EXPIRATION_DATE, expirationDate.Ticks);
		}
		else {
			return this.SetHeader(IntegrationMessageHeaderAccessor.EXPIRATION_DATE, null);
		}
	}

	public AbstractIntegrationMessageBuilder<T> SetCorrelationId(Object correlationId) {
		return this.SetHeader(IntegrationMessageHeaderAccessor.CORRELATION_ID, correlationId);
	}

	public AbstractIntegrationMessageBuilder<T> PushSequenceDetails(Object correlationId, int sequenceNumber, int sequenceSize) {
		Object incomingCorrelationId = this.GetCorrelationId();
		List<List<Object>> incomingSequenceDetails = this.GetSequenceDetails();
		if (incomingCorrelationId != null) {
			if (incomingSequenceDetails == null) {
				incomingSequenceDetails = new List<List<object>>();
			}
			else {
				incomingSequenceDetails = new List<List<Object>>(incomingSequenceDetails);
			}
			incomingSequenceDetails.Add(new List<object>{
					incomingCorrelationId,
					this.GetSequenceNumber(), 
					this.GetSequenceSize()
			});
		}
		if (incomingSequenceDetails != null) {
			this.SetHeader(IntegrationMessageHeaderAccessor.SEQUENCE_DETAILS, incomingSequenceDetails);
		}
		return SetCorrelationId(correlationId).SetSequenceNumber(sequenceNumber).SetSequenceSize(sequenceSize);
	}

	public AbstractIntegrationMessageBuilder<T> PopSequenceDetails() {
		List<List<object>> incomingSequenceDetails = this.GetSequenceDetails();
		if (incomingSequenceDetails == null) {
			return this;
		}
		else {
			incomingSequenceDetails = new List<List<object>>(incomingSequenceDetails);
		}
		List<object> sequenceDetails = incomingSequenceDetails[incomingSequenceDetails.Count - 1];
		incomingSequenceDetails.RemoveAt(incomingSequenceDetails.Count - 1);
		
		if (sequenceDetails.Count == 3)
		throw new InvalidOperationException("Wrong sequence details (not created by MessageBuilder?): " + sequenceDetails);
		
		SetCorrelationId(sequenceDetails[0]);
		int? sequenceNumber = (int?)sequenceDetails[1];
		int? sequenceSize = (int?) sequenceDetails[2];
		if (sequenceNumber != null) {
			SetSequenceNumber(sequenceNumber.Value);
		}
		if (sequenceSize != null) {
			SetSequenceSize(sequenceSize.Value);
		}
		if (incomingSequenceDetails.Count > 0) {
			this.SetHeader(IntegrationMessageHeaderAccessor.SEQUENCE_DETAILS, incomingSequenceDetails);
		}
		else {
			this.RemoveHeader(IntegrationMessageHeaderAccessor.SEQUENCE_DETAILS);
		}
		return this;
	}

	protected abstract List<List<Object>> GetSequenceDetails();

	protected abstract Object GetCorrelationId();

	protected abstract Object GetSequenceNumber();

	protected abstract Object GetSequenceSize();

	public AbstractIntegrationMessageBuilder<T> SetReplyChannel(MessageChannel replyChannel) {
		return this.SetHeader(MessageHeaders.REPLY_CHANNEL, replyChannel);
	}

	public AbstractIntegrationMessageBuilder<T> SetReplyChannelName(String replyChannelName) {
		return this.SetHeader(MessageHeaders.REPLY_CHANNEL, replyChannelName);
	}

	public AbstractIntegrationMessageBuilder<T> SetErrorChannel(MessageChannel errorChannel) {
		return this.SetHeader(MessageHeaders.ERROR_CHANNEL, errorChannel);
	}

	public AbstractIntegrationMessageBuilder<T> SetErrorChannelName(String errorChannelName) {
		return this.SetHeader(MessageHeaders.ERROR_CHANNEL, errorChannelName);
	}

	public AbstractIntegrationMessageBuilder<T> SetSequenceNumber(int sequenceNumber) {
		return this.SetHeader(IntegrationMessageHeaderAccessor.SEQUENCE_NUMBER, sequenceNumber);
	}

	public AbstractIntegrationMessageBuilder<T> SetSequenceSize(int sequenceSize) {
		return this.SetHeader(IntegrationMessageHeaderAccessor.SEQUENCE_SIZE, sequenceSize);
	}

	public AbstractIntegrationMessageBuilder<T> SetPriority(int priority) {
		return this.SetHeader(IntegrationMessageHeaderAccessor.PRIORITY, priority);
	}

	public abstract Message<T> Build();
	}
	
}