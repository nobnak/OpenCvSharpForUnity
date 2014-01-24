using System;
using System.Diagnostics;

namespace nobnak.Util {

	public static class HighResTime {
	    private static DateTime _startTime;
	    private static Stopwatch _stopWatch = null;
	    private static TimeSpan _maxIdle = TimeSpan.FromSeconds(10);

	    public static DateTime UtcNow {
	        get {
	            if ((_stopWatch == null) || (_startTime.Add(_maxIdle) < DateTime.UtcNow)) {
	                Reset();
	            }
	            return _startTime.AddTicks(_stopWatch.ElapsedTicks);
	        }
	    }

	    private static void Reset() {
	        _startTime = DateTime.UtcNow;
	        _stopWatch = Stopwatch.StartNew();
	    }
	}

}