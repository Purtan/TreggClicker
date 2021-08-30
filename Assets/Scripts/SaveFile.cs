using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[System.Serializable]
public class SaveFile {
    public string treggCount;
    public string lastSave;

    public BigInteger GetTreggCount() {
        BigInteger result;
        if(BigInteger.TryParse(treggCount, out result)) {
            return result;
        } else {
            Debug.Log("Couldn't parse " + treggCount + " to BigInteger");
            return new BigInteger();
        }
    }

    public string GetTreggCountString() {
        return treggCount.ToString();
    }

    public void SetTreggCount(string count) {
        BigInteger result;
        if(BigInteger.TryParse(count, out result)) {
            treggCount = result.ToString();
        } else {
            Debug.Log("Unable to parse " + count + " to BigInteger");
        }
    }

    public override string ToString() {
        return JsonUtility.ToJson(this);
    }
}
