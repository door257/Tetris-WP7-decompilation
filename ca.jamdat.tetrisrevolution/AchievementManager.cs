using System;
using ca.jamdat.flight;
using Microsoft.Xna.Framework.GamerServices;
using Tetris;

namespace ca.jamdat.tetrisrevolution
{
	public static class AchievementManager
	{
		private static object mAchievementsLockObject = new object();

		public static void Update()
		{
			UpdateCareerAchievements();
			UpdateAdvancedAchievements();
		}

		private static void UpdateCareerAchievements()
		{
			for (int i = 0; i != 6; i++)
			{
				if (FeatsExpert.Get().IsCareerFeatUnlocked((sbyte)i))
				{
					AwardAchievement(GetStringFromCareerAchievementId(i));
				}
			}
		}

		private static void UpdateAdvancedAchievements()
		{
			for (int i = 0; i != 5; i++)
			{
				if (FeatsExpert.Get().IsAdvancedFeatUnlocked((sbyte)i))
				{
					AwardAchievement(GetStringFromAdvancedAchievementId(i));
				}
			}
		}

		private static void AwardAchievement(string achievementKey)
		{
			FlLog.Log("Awarding Achievement " + achievementKey);
			try
			{
				if (LiveState.Gamer == null)
				{
					return;
				}
				AchievementCollection achievements = LiveState.Gamer.GetAchievements();
				lock (mAchievementsLockObject)
				{
					Achievement achievement = GetAchievement(achievements, achievementKey);
					if (achievement != null && !achievement.IsEarned)
					{
						LiveState.Gamer.BeginAwardAchievement(achievementKey, AwardAchievementCallback, LiveState.Gamer);
					}
				}
			}
			catch (Exception exception)
			{
				FlLog.Log(exception);
			}
		}

		private static Achievement GetAchievement(AchievementCollection acol, string achievementKey)
		{
			if (acol != null)
			{
				foreach (Achievement item in acol)
				{
					if (item.Key == achievementKey)
					{
						return item;
					}
				}
			}
			return null;
		}

		private static void AwardAchievementCallback(IAsyncResult result)
		{
			SignedInGamer signedInGamer = result.AsyncState as SignedInGamer;
			if (signedInGamer != null)
			{
				signedInGamer.EndAwardAchievement(result);
			}
		}

		private static string GetStringFromCareerAchievementId(int id)
		{
			string result = null;
			switch (id)
			{
			case 0:
				result = "Tetriminator";
				break;
			case 1:
				result = "Millenium";
				break;
			case 2:
				result = "Waterfall";
				break;
			case 3:
				result = "Spinmaster";
				break;
			case 4:
				result = "Tenacious";
				break;
			case 5:
				result = "Master";
				break;
			}
			return result;
		}

		private static string GetStringFromAdvancedAchievementId(int id)
		{
			string result = null;
			switch (id)
			{
			case 0:
				result = "WayOver";
				break;
			case 1:
				result = "Expert";
				break;
			case 2:
				result = "Dodecadent";
				break;
			case 3:
				result = "OnARoll";
				break;
			case 4:
				result = "Frenzy";
				break;
			}
			return result;
		}
	}
}
