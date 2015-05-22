using UnityEngine;
using System.Collections;

public class Status  {
		float currentduration;
		bool active;
		string name;
		
		public Status(string name)
		{
			this.name = name;
			currentduration = 0;
			active = false;
		}
		
		public void addDuration(float addDuration)
		{
			currentduration += addDuration;
			if (currentduration > 0f)
				active = true;
		}
		
		public bool isActive()
		{
			return active;
		}
		
		public void decreaseDuration(float amount)
		{
			if (active) 
			{
				currentduration -= amount;
				if (currentduration <= 0) 
				{
					currentduration = 0;
					active = false;
				}
			}
		}
		
		public string getStringIntegerDuration()
		{
			return Mathf.RoundToInt (currentduration).ToString();
		}
		public string getString2DecimalsDuration()
		{
			return (Mathf.Round (currentduration * 100f) / 100f).ToString();
		}
		
		public string getName()
		{
			return name;
		}

}
