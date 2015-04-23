using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Text.RegularExpressions;
using System.Linq;
using Android.Gms.Ads;

namespace HaikuChecker
{
	[Activity (Label = "HaikuChecker", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{
		

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			//Get textboxes and labels for user input
			TextView firstLine = FindViewById<TextView> (Resource.Id.firstLine);
			TextView secondLine = FindViewById<TextView> (Resource.Id.secondLine);
			TextView thirdLine = FindViewById<TextView> (Resource.Id.thirdLine);
			TextView isHaikuText = FindViewById<TextView> (Resource.Id.isHaikuText);

			//Get admob control
			AdView adView = FindViewById<AdView> (Resource.Id.adView);
			AdRequest adRequest = new AdRequest.Builder ().Build ();
			adView.LoadAd (adRequest);

			// Get the action button from the layout
			Button button = FindViewById<Button> (Resource.Id.myButton);

			// Attach a click event to the action button
			button.Click += delegate {
				

				int[] haiku = new int[3];

				haiku[0] = SyllableCount(firstLine.Text);
				haiku[1] = SyllableCount(secondLine.Text);
				haiku[2] = SyllableCount(thirdLine.Text);

				if(haiku[0] == 5 && haiku[1] == 7 && haiku[2] == 5)
					isHaikuText.Text= "YES";
				else
					isHaikuText.Text= "NO";
			};
		}




		private static int SyllableCount(string words)
		{
			words = words.ToLower();
			string[] checkWords = words.Split(' ');
			int totalSyllables = 0;
			foreach (string word in checkWords)
			{
				totalSyllables += numberOfSyllables(word);
			}
			return totalSyllables;
		}

		public static int numberOfSyllables(String word)
		{
			int syllabeleCount = 0;
			bool lastWasVowel = false;
			string lowerCase = word.ToLower().Replace("ome", "um").Replace("ime", "im").Replace("imn", "imen").Replace("ine", "in").Replace("ely", "ly").Replace("ure", "ur").Replace("ery", "ry");
			for (int n = 0; n < lowerCase.Length; n++)
			{
				if (isVowel(lowerCase[n]))
				{
					if (!lastWasVowel)
						syllabeleCount++;
					lastWasVowel = true;
				}
				else
				{
					lastWasVowel = false;
				}
			}
			if (lowerCase.EndsWith("ing") || lowerCase.EndsWith("ings"))
			{
				if (lowerCase.Length > 4 && isVowel(lowerCase[lowerCase.Length - 4]))
					syllabeleCount++;
			}
			if (lowerCase.EndsWith("e") && !lowerCase.EndsWith("le"))
			{
				syllabeleCount--;
			}
			if (lowerCase.EndsWith("es"))
			{
				if (lowerCase.Length > 4 && isVowel(lowerCase[lowerCase.Length - 4]))
					syllabeleCount--;
			}
			if (lowerCase.EndsWith("e's"))
			{
				if (lowerCase.Length > 5 && isVowel(lowerCase[lowerCase.Length - 5]))
					syllabeleCount--;
			}
			if (lowerCase.EndsWith("ed") && !lowerCase.EndsWith("ted") && !lowerCase.EndsWith("ded"))
			{
				syllabeleCount--;
			}
			return syllabeleCount > 0 ? syllabeleCount : 1;
		}

		public static bool isVowel(char c)
		{
			return c == 'a' || c == 'e' || c == 'i' || c == 'o' || c == 'u' || c == 'y';
		}
	}
}


