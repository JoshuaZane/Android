package parse;


public class ParsePushBroadcastReceiver
	extends android.content.BroadcastReceiver
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceive:(Landroid/content/Context;Landroid/content/Intent;)V:GetOnReceive_Landroid_content_Context_Landroid_content_Intent_Handler\n" +
			"";
		mono.android.Runtime.register ("Parse.ParsePushBroadcastReceiver, Parse.Android, Version=1.5.5.0, Culture=neutral, PublicKeyToken=null", ParsePushBroadcastReceiver.class, __md_methods);
	}


	public ParsePushBroadcastReceiver () throws java.lang.Throwable
	{
		super ();
		if (getClass () == ParsePushBroadcastReceiver.class)
			mono.android.TypeManager.Activate ("Parse.ParsePushBroadcastReceiver, Parse.Android, Version=1.5.5.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onReceive (android.content.Context p0, android.content.Intent p1)
	{
		n_onReceive (p0, p1);
	}

	private native void n_onReceive (android.content.Context p0, android.content.Intent p1);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
