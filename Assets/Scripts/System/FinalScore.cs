using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Record
{
    public string name;
    public int score;

    public Record(string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}

public class FinalScore : MonoBehaviour
{
    public Score score;
    public Text finalScoreText;
    public Text highScoreText;
    public InputField playerName;

    private int recordCount;
    private List<Record> records;

    void Start()
    {
        score.isCounting = false;

        InitializeRecord();

        finalScoreText.text = score.money.ToString("C0");
    }

    void InitializeRecord()
    {
        records = new List<Record>();
        recordCount = PlayerPrefs.GetInt("RecordCount");

        for (int i = 0; i < recordCount; i++)
        {
            var name = PlayerPrefs.GetString($"Record{i}.name");
            var score = PlayerPrefs.GetInt($"Record{i}.score");
            records.Add(new Record(name, score));
        }

        UpdateRecordPanel();
    }

    void UpdateRecordPanel()
    {
        records = records.OrderByDescending(x => x.score).Take(8).ToList();

        var str = "";
        for (int i = 0; i < recordCount; i++)
        {
            str += $"{i + 1}. {records[i].name} : {records[i].score}\n";
        }
        highScoreText.text = str;
    }

    public void StoreRecord()
    {
        Record newRecord = new Record(playerName.text, score.money);

        PlayerPrefs.SetString($"Record{recordCount}.name", newRecord.name);
        PlayerPrefs.SetInt($"Record{recordCount}.score", newRecord.score);

        recordCount++;
        PlayerPrefs.SetInt("RecordCount", recordCount);

        records.Add(newRecord);

        UpdateRecordPanel();
    }

    public void ClearRecord()
    {
        
        PlayerPrefs.DeleteKey("RecordCount");
        for (int i = 0; i < recordCount; i++)
        {
            PlayerPrefs.DeleteKey($"Record{i}.name");
            PlayerPrefs.DeleteKey($"Record{i}.score");
        }
        recordCount = 0;
        highScoreText.text = "";
    }
}
