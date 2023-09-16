using S22.Imap;
using System.Net.Mail;

public class MailManager
{
	public int MaxThreads { get; set; } = 3;
	protected ImapClient client { get; set; }
	public IEnumerable<uint>? messageIds { get; set; }
	public IEnumerable<MailMessage>? headers { get; set; }

	public MailManager( string _username, string _password )
	{
		client = new("imap.gmail.com", 993, _username, _password, AuthMethod.Login, true);
	}

	public int Search( SearchCondition sc ) {
		messageIds = client.Search( sc );
		return messageIds.Count();
	}

	

	protected IEnumerable<MailMessage> getMessages( IEnumerable<uint> _ids, FetchOptions _fo, Boolean _seen=false, string? _mailbox=null ) {
		return client.GetMessages(_ids, _fo, _seen, _mailbox);
	}

	protected async Task<IEnumerable<MailMessage>> getMessagesAsync(IEnumerable<uint> _ids, FetchOptions _fo, Boolean _seen = false, string? _mailbox = null) {
		return await Task.Run(() => { return client.GetMessages(_ids, _fo, _seen, _mailbox); });
	}


    public IEnumerable<MailMessage> GetThreadedMessages(  IEnumerable<uint> _ids, FetchOptions _fo, Boolean _seen=false, string? _mailbox=null) {
		
		if ( messageIds?.Count() < 1 ) {
			throw new ArgumentNullException("Must use search first");
		}

		Task<IEnumerable<MailMessage>>[] tasks = new Task<IEnumerable<MailMessage>>[MaxThreads];
		IEnumerable<MailMessage> messages = new List<MailMessage>();
		Range range;
		int batchsize = _ids.Count() / MaxThreads;
		batchsize = ( batchsize == 0 ) ? 1: batchsize;
		int start = 0;
		int end = start + batchsize;

	    uint[] IDArray = _ids.ToArray();

		for (int i = 0; i < tasks.Length; i++) {

			if (i + 1 == tasks.Length) {
				range = new Range(start, ^0);
			}
			else {
				range = new Range(start, end);
			}
			
			tasks[i] = getMessagesAsync(IDArray[range], _fo, _seen, _mailbox);
			start = end;
			end += batchsize;
		}

		Task.WaitAll(tasks);

		foreach( Task<IEnumerable<MailMessage>> task in tasks) {
            messages = messages.Concat(task.Result);
        }

		Console.WriteLine( _ids.Count() );
		Console.WriteLine( messages.Count() );

		return messages;

	}

	~MailManager() {
		client.Dispose();
	}

}
