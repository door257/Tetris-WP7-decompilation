namespace ca.jamdat.flight
{
	public class KeyFrameSequence
	{
		public const sbyte typeNumber = 89;

		public const sbyte typeID = 89;

		public const bool supportsDynamicSerialization = false;

		public const sbyte FieldKeysPerChannel = 0;

		public const sbyte FieldValuesPerKey = 1;

		public const sbyte FieldBytesPerValue = 2;

		public const sbyte FieldPointPosition = 3;

		public const sbyte FieldTimePointPosition = 4;

		public const sbyte FieldSingleInterpolator = 5;

		public const sbyte FieldLoopingPeriod = 6;

		public const sbyte FieldLastIndex = 7;

		public const sbyte FieldTimeValues = 8;

		public static int[] mValueBuffer = new int[8];

		public short[] mValues;

		public KeyFrameSequence()
		{
		}

		public static KeyFrameSequence Cast(object o, KeyFrameSequence _)
		{
			return (KeyFrameSequence)o;
		}

		public KeyFrameSequence(short numKeys, sbyte bytesPerValue, sbyte pointPosition, sbyte valuesPerKey)
		{
			int num = numKeys * bytesPerValue * valuesPerKey;
			short[] array = new short[8 + numKeys + (num + 1 >> 1)];
			array[0] = numKeys;
			array[2] = bytesPerValue;
			array[3] = pointPosition;
			array[4] = 0;
			array[1] = valuesPerKey;
			array[5] = 1;
			array[6] = 0;
			array[7] = -1;
			mValues = array;
		}

		public virtual void destruct()
		{
			mValues = null;
		}

		public virtual void GetObjectValue(int currentTimeMs, int[] buffer)
		{
			short num = mValues[1];
			int num2 = mValues[6] << (int)mValues[4];
			int[] array = mValueBuffer;
			if (num2 != 0)
			{
				currentTimeMs = FlMath.Modulo(currentTimeMs, num2);
			}
			int keyFrameIndex = GetKeyFrameIndex(currentTimeMs);
			GetKeyFrameValue(keyFrameIndex, array, 0);
			if (mValues[5] == 1)
			{
				GetKeyFrameValue(keyFrameIndex + 1, array, 4);
				int correctedIndex = GetCorrectedIndex(keyFrameIndex);
				int correctedIndex2 = GetCorrectedIndex(keyFrameIndex + 1);
				int num3 = mValues[8 + correctedIndex] << (int)mValues[4];
				int num4 = mValues[8 + correctedIndex2] << (int)mValues[4];
				if (correctedIndex > correctedIndex2)
				{
					if (currentTimeMs < num4)
					{
						currentTimeMs += num2;
					}
					num4 += num2;
				}
				int num5 = num4 - num3;
				if (num5 != 0)
				{
					int num6 = FlMath.Absolute(num5 >> 1);
					for (int i = 0; i < num; i++)
					{
						int num7 = (num4 - currentTimeMs) * array[i] + (currentTimeMs - num3) * array[4 + i];
						num7 += ((num7 < 0) ? (-num6 + 1) : num6);
						array[i] = num7 / num5;
					}
				}
			}
			for (int j = 0; j < num; j++)
			{
				buffer[j] = array[j];
			}
		}

		public virtual void SetKeyFrame(int index, int timeMs, int[] value)
		{
			ExtendTimeValues(timeMs);
			mValues[8 + index] = (short)(timeMs >> (int)mValues[4]);
			for (int i = 0; i < mValues[1]; i++)
			{
				value[i] >>= -mValues[3];
			}
			SetKeyFrameValue(index, value);
		}

		public virtual void SetKeyFrame(int index, int timeMs, F32[] value, int point)
		{
			ExtendTimeValues(timeMs);
			mValues[8 + index] = (short)(timeMs >> (int)mValues[4]);
			int[] array = new int[mValues[1]];
			int num = point - mValues[3];
			for (int i = 0; i < mValues[1]; i++)
			{
				if (num < 0)
				{
					array[i] = value[i].ToFixedPoint(mValues[3]) << -num;
				}
				else
				{
					array[i] = value[i].ToFixedPoint(mValues[3]) >> num;
				}
			}
			SetKeyFrameValue(index, array);
		}

		public virtual sbyte GetInterpolator()
		{
			return (sbyte)mValues[5];
		}

		public virtual void SetInterpolator(sbyte interpolator)
		{
			mValues[5] = interpolator;
		}

		public virtual bool IsLooping()
		{
			return mValues[6] != 0;
		}

		public virtual int GetPeriod()
		{
			return mValues[6] << (int)mValues[4];
		}

		public virtual void SetPeriod(int val)
		{
			ExtendTimeValues(val);
			mValues[6] = (short)(val >> (int)mValues[4]);
		}

		public virtual KeyFrameSequence OnSerialize(Package p)
		{
			short t = 0;
			short[] t2 = mValues;
			t = p.SerializeIntrinsic(t);
			t2 = p.SerializeIntrinsics(t2, t);
			mValues = t2;
			return this;
		}

		public virtual void ExtendTimeValues(int timeMs)
		{
			short num = 0;
			while (timeMs < -32768 || timeMs >= 32768)
			{
				num = (short)(num + 1);
				timeMs >>= 1;
			}
			if (num > mValues[4])
			{
				for (int i = 0; i < mValues[0]; i++)
				{
					ref short reference = ref mValues[8 + i];
					reference = (short)(reference >> num - mValues[4]);
				}
				ref short reference2 = ref mValues[6];
				reference2 = (short)(reference2 >> num - mValues[4]);
				mValues[4] = num;
			}
		}

		public virtual int GetKeyFrameIndex(int currentTimeMs)
		{
			short[] array = mValues;
			if (array[7] != -1)
			{
				short num = array[7];
				short num2 = (short)((num < array[0] - 1) ? ((short)(num + 1)) : 0);
				if (array[8 + num] << (int)mValues[4] <= currentTimeMs && (num2 == 0 || array[8 + num2] << (int)mValues[4] > currentTimeMs))
				{
					return num;
				}
				short num3 = (short)((num2 < array[0] - 1) ? ((short)(num2 + 1)) : 0);
				if (array[8 + num2] << (int)mValues[4] <= currentTimeMs && (num3 == 0 || array[8 + num3] << (int)mValues[4] > currentTimeMs))
				{
					array[7] = num2;
					return num2;
				}
			}
			int num4 = array[0] >> 1;
			int num5 = array[0] - 1;
			int num6 = 0;
			while (num5 != num6)
			{
				int num7 = array[8 + num4] << (int)mValues[4];
				if (num7 < currentTimeMs)
				{
					num6 = num4;
					int num8 = num5 - num6 >> 1;
					num4 += ((num8 <= 0) ? 1 : num8);
					continue;
				}
				if (num7 > currentTimeMs)
				{
					num5 = num4 - 1;
					int num8 = num5 - num6 >> 1;
					num4 = num5 - num8;
					continue;
				}
				array[7] = (short)num4;
				return num4;
			}
			if (num6 == 0 && array[8] << (int)mValues[4] > currentTimeMs)
			{
				array[7] = -1;
				if (array[6] == 0 || array[6] == currentTimeMs)
				{
					return -1;
				}
				return GetKeyFrameIndex(array[6]);
			}
			array[7] = (short)num6;
			return num6;
		}

		public virtual void GetKeyFrameValue(int index, int[] buffer, int bufferStartIndex)
		{
			short[] array = mValues;
			short num = array[0];
			short num2 = array[2];
			short num3 = array[1];
			int num4 = 8 + num;
			index = GetCorrectedIndex(index) * num2 * num3;
			if (num2 > 1)
			{
				index = (index >> 1) + num4;
			}
			for (int i = 0; i < num3; i++)
			{
				int num5 = 0;
				num5 = ((num2 != 2) ? ((sbyte)(array[num4 + (index >> 1)] >> ((~index & 1) << 3))) : array[index]);
				index++;
				buffer[i + bufferStartIndex] = num5;
			}
		}

		public virtual void SetKeyFrameValue(int arrayIndex, int[] buffer)
		{
			short[] array = mValues;
			int num = 8 + array[0];
			int num2 = array[2];
			arrayIndex *= num2 * array[1];
			if (num2 > 1)
			{
				arrayIndex = (arrayIndex >> 1) + num;
			}
			for (int i = 0; i < array[1]; i++)
			{
				int num3 = buffer[i];
				if (num2 == 2)
				{
					array[arrayIndex] = (short)num3;
				}
				else
				{
					int num4 = num + (arrayIndex >> 1);
					array[num4] = (short)((array[num4] & (255 << ((arrayIndex & 1) << 3))) | ((num3 & 0xFF) << ((~arrayIndex & 1) << 3)));
				}
				arrayIndex++;
			}
		}

		public virtual F32 GetTimeRatio(int currentTimeMs, int indexA, int indexB)
		{
			short[] array = mValues;
			int num = array[8 + indexA] << (int)mValues[4];
			int num2 = 0;
			if (indexB >= array[0])
			{
				if (array[6] != 0)
				{
					num2 = (array[6] << (int)mValues[4]) - num + (array[8] << (int)mValues[4]);
					if (currentTimeMs < num)
					{
						currentTimeMs += array[6] << (int)mValues[4];
					}
				}
			}
			else
			{
				num2 = (array[8 + indexB] << (int)mValues[4]) - num;
			}
			if (num2 == 0)
			{
				return F32.One(16);
			}
			return F32.FromInt(currentTimeMs - num, 16).Div(num2);
		}

		public virtual int GetCorrectedIndex(int index)
		{
			int num = mValues[0];
			if (index < 0)
			{
				index = ((mValues[6] != 0) ? (num - 1) : 0);
			}
			else if (index >= num)
			{
				index = ((mValues[6] == 0) ? (num - 1) : 0);
			}
			return index;
		}

		public static KeyFrameSequence[] InstArrayKeyFrameSequence(int size)
		{
			KeyFrameSequence[] array = new KeyFrameSequence[size];
			for (int i = 0; i < size; i++)
			{
				array[i] = new KeyFrameSequence();
			}
			return array;
		}

		public static KeyFrameSequence[][] InstArrayKeyFrameSequence(int size1, int size2)
		{
			KeyFrameSequence[][] array = new KeyFrameSequence[size1][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyFrameSequence[size2];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyFrameSequence();
				}
			}
			return array;
		}

		public static KeyFrameSequence[][][] InstArrayKeyFrameSequence(int size1, int size2, int size3)
		{
			KeyFrameSequence[][][] array = new KeyFrameSequence[size1][][];
			for (int i = 0; i < size1; i++)
			{
				array[i] = new KeyFrameSequence[size2][];
				for (int j = 0; j < size2; j++)
				{
					array[i][j] = new KeyFrameSequence[size3];
					for (int k = 0; k < size3; k++)
					{
						array[i][j][k] = new KeyFrameSequence();
					}
				}
			}
			return array;
		}
	}
}
