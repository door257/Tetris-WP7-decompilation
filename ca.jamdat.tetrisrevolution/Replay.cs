using ca.jamdat.flight;

namespace ca.jamdat.tetrisrevolution
{
	public class Replay
	{
		public const int replayModeRecording = 0;

		public const int replayModePlayback = 1;

		public const int replayModeCount = 2;

		public int mReplayMode;

		public sbyte mGameMode;

		public int mDifficulty;

		public int mNumberOfLineToComplete;

		public int mTimeAllocated;

		public int mTotalTime;

		public int mTetrisMiniVariationType;

		public int mRandomState;

		public ReplayEvent[] mEventQueue;

		public int mEventQueueLength;

		public int mCurrentEventIdx;

		public int mOnTimeQueueLength;

		public int mCurrentOnTimeIdx;

		public static Replay Get()
		{
			return GameApp.Get().GetReplay();
		}

		public Replay()
		{
			mReplayMode = 2;
			mGameMode = 1;
			mDifficulty = -1;
			mTetrisMiniVariationType = -1;
			mEventQueue = new ReplayEvent[40000];
			for (int i = 0; i < 40000; i++)
			{
				mEventQueue[i] = null;
			}
		}

		public virtual void destruct()
		{
			Reset();
			mEventQueue = null;
		}

		public virtual void SetGameParameters(int gameType, GameParameter gameParameter)
		{
			if (mReplayMode == 0)
			{
				mDifficulty = gameParameter.GetDifficulty();
				mNumberOfLineToComplete = gameParameter.GetLineLimit();
				mTimeAllocated = gameParameter.GetTimeLimit();
				mTetrisMiniVariationType = gameType;
				mRandomState = GameRandom.GetCurrentRandomState();
				mTotalTime = 0;
			}
			else
			{
				GameRandom.InitSeed(mRandomState);
			}
		}

		public virtual int GetDifficulty()
		{
			return mDifficulty;
		}

		public virtual int GetTimeLimit()
		{
			return mTimeAllocated;
		}

		public virtual int GetLineLimit()
		{
			return mNumberOfLineToComplete;
		}

		public virtual int GetGameType()
		{
			return mTetrisMiniVariationType;
		}

		public virtual sbyte GetGameMode()
		{
			return mGameMode;
		}

		public virtual void SetGameMode(sbyte gameMode)
		{
			if (mReplayMode == 0)
			{
				mGameMode = gameMode;
			}
		}

		public virtual void SetReplayMode(int replayMode)
		{
			mReplayMode = replayMode;
			if (mReplayMode == 1)
			{
				GameRandom.InitSeed(mRandomState);
			}
		}

		public virtual bool IsRecording()
		{
			return false;
		}

		public virtual bool IsPlaying()
		{
			return mReplayMode == 1;
		}

		public virtual bool IsReplayLoaded()
		{
			return mEventQueueLength != 0;
		}

		public virtual ReplayEvent AddEvent(int eventType, int playTimeMS)
		{
			if (IsRecording())
			{
				ReplayEvent replayEvent = ReplayEventFactory.CreateReplayEvent(eventType, playTimeMS);
				AddEvent(replayEvent);
				UpdateReplayBackupFile();
				return replayEvent;
			}
			return null;
		}

		public virtual void AddOnTimeEvent(int deltaTimes, int totalGameTime)
		{
			if (IsRecording())
			{
				mOnTimeQueueLength++;
				mTotalTime = totalGameTime;
				UpdateReplayBackupFile();
			}
		}

		public virtual int PopDeltaTime()
		{
			int result = 0;
			if (mCurrentOnTimeIdx < mOnTimeQueueLength)
			{
				result = 30;
				mCurrentOnTimeIdx++;
			}
			return result;
		}

		public virtual int PeekDeltaTime()
		{
			int result = 0;
			if (mCurrentOnTimeIdx < mOnTimeQueueLength)
			{
				result = 30;
			}
			return result;
		}

		public virtual int GetCurrentTimeIndex()
		{
			int result = mOnTimeQueueLength;
			if (IsPlaying())
			{
				result = mCurrentOnTimeIdx;
			}
			return result;
		}

		public virtual bool IsTimeQueueEmpty()
		{
			return mCurrentOnTimeIdx >= mOnTimeQueueLength;
		}

		public virtual bool HasNextEvent()
		{
			return PeekEvent() != null;
		}

		public virtual ReplayEvent PeekEvent()
		{
			if (mCurrentEventIdx < mEventQueueLength)
			{
				return mEventQueue[mCurrentEventIdx];
			}
			return null;
		}

		public virtual ReplayEvent PopEvent()
		{
			if (mCurrentEventIdx < mEventQueueLength)
			{
				return mEventQueue[mCurrentEventIdx++];
			}
			return null;
		}

		public virtual ReplayEvent GetLastEvent()
		{
			return mEventQueue[mEventQueueLength - 1];
		}

		public virtual void Reset()
		{
			mDifficulty = -1;
			mNumberOfLineToComplete = 0;
			mTimeAllocated = 0;
			mTetrisMiniVariationType = -1;
			mCurrentEventIdx = 0;
			mEventQueueLength = 0;
			mRandomState = 0;
			mTotalTime = 0;
			for (int i = 0; i < 40000; i++)
			{
				if (mEventQueue[i] != null)
				{
					mEventQueue[i] = null;
					mEventQueue[i] = null;
				}
			}
			mCurrentOnTimeIdx = 0;
			mOnTimeQueueLength = 0;
		}

		public virtual void RestartReplay()
		{
			mCurrentEventIdx = 0;
			mCurrentOnTimeIdx = 0;
			GameTimeSystem.Reset();
		}

		public virtual void Read(FileSegmentStream fileStream)
		{
			if (fileStream.HasValidData())
			{
				FileReader fileReader = FileSerializationFactory.CreateReader(fileStream);
				ReadFileReader(fileReader);
				fileReader = null;
			}
		}

		public virtual void Write(FileSegmentStream fileStream)
		{
			FileWriter fileWriter = FileSerializationFactory.CreateWriter(fileStream);
			WriteFileWriter(fileWriter);
			fileStream.SetValidDataFlag(true);
			fileWriter = null;
		}

		public virtual void ValidateFileWriting(FileReader fileReader)
		{
		}

		public virtual void UpdateReplayBackupFile()
		{
		}

		public virtual void LoadReplayFromBlob(Blob blobFile)
		{
			FileReader fileReader = FileSerializationFactory.CreateReader(blobFile);
			ReadFileReader(fileReader);
			fileReader = null;
		}

		public virtual bool HasAvailableSpace()
		{
			return mEventQueueLength + 1 < 40000;
		}

		public virtual bool AddEvent(ReplayEvent replayEvent)
		{
			if (HasAvailableSpace())
			{
				mEventQueue[mEventQueueLength] = replayEvent;
				mEventQueueLength++;
				return true;
			}
			return false;
		}

		public virtual void ReadFileReader(FileReader fileReader)
		{
			fileReader.Start();
			fileReader.ReadLong();
			mGameMode = fileReader.ReadByte();
			mDifficulty = fileReader.ReadShort();
			mNumberOfLineToComplete = fileReader.ReadLong();
			mTimeAllocated = fileReader.ReadLong();
			mTetrisMiniVariationType = fileReader.ReadShort();
			mTotalTime = fileReader.ReadLong();
			mRandomState = fileReader.ReadLong();
			mEventQueueLength = fileReader.ReadLong();
			mOnTimeQueueLength = fileReader.ReadLong();
			for (int i = 0; i < mEventQueueLength; i++)
			{
				mEventQueue[i] = null;
				int replayEventType = fileReader.ReadByte();
				int timeRecorded = fileReader.ReadLong();
				mEventQueue[i] = ReplayEventFactory.CreateReplayEvent(replayEventType, timeRecorded);
			}
			mCurrentEventIdx = 0;
			mCurrentOnTimeIdx = 0;
			fileReader.End();
		}

		public virtual void WriteFileWriter(FileWriter fileWriter)
		{
			fileWriter.Start();
			fileWriter.WriteLong(6);
			fileWriter.WriteByte(mGameMode);
			fileWriter.WriteShort((short)mDifficulty);
			fileWriter.WriteLong(mNumberOfLineToComplete);
			fileWriter.WriteLong(mTimeAllocated);
			fileWriter.WriteShort((short)mTetrisMiniVariationType);
			fileWriter.WriteLong(mTotalTime);
			fileWriter.WriteLong(mRandomState);
			fileWriter.WriteLong(mEventQueueLength);
			fileWriter.WriteLong(mOnTimeQueueLength);
			for (int i = 0; i < mEventQueueLength; i++)
			{
				ReplayEvent replayEvent = mEventQueue[i];
				fileWriter.WriteByte((sbyte)replayEvent.GetType());
				fileWriter.WriteLong(replayEvent.GetTime());
			}
			fileWriter.End();
		}

		public static Replay[] InstArrayReplay(int size)
		{
			Replay[] array = new Replay[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new Replay();
			}
			return array;
		}

		public static Replay[][] InstArrayReplay(int size1, int size2)
		{
			Replay[][] array = new Replay[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Replay[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Replay();
				}
			}
			return array;
		}

		public static Replay[][][] InstArrayReplay(int size1, int size2, int size3)
		{
			Replay[][][] array = new Replay[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new Replay[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new Replay[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new Replay();
					}
				}
			}
			return array;
		}
	}
}
