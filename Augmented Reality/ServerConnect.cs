using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Vuforia;
using UnityEngine;

public class ServerConnect : MonoBehaviour {

    //Entire Statue
    public class Statue
    {
        public ObjectId _id { get; set; }
        public string vumark { get; set; }
        public string name { get; set; }
        public Trans namePos { get; set; }
        public string birth { get; set; }
        public string death { get; set; }
        public string bio { get; set; }
		public Special s { get; set; }
        public Trans infoTrans { get; set; }
		public string videoURL { get; set; }
        public Trans videoTrans { get; set; }
    }

    //Transform of Items
    public class Trans
    {
        public ObjectId _id { get; set; }
        public float x { get; set; }
        public float y { get; set; }
        public float z { get; set; }
        public float xScale { get; set; }
        public float yScale { get; set; }
        public float zScale { get; set; }
        public float xRot { get; set; }
        public float yRot { get; set; }
        public float zRot { get; set; }
    }

    //special Item
    public class Special
    {
        public ObjectId _id { get; set; }
        public string name { get; set; }
        public Trans trans { get; set; }
    }

    //Mongo Connection
    private static MongoClient client;
    private static MongoServer server;
    private static MongoDatabase db;
    private static MongoCollection<Statue> coll;

    //vumarks
	public VuMarkBehaviour vb;
	public VuMarkManager vmm;
	public VuMarkTarget vm;
    
	public GameObject steam;
    public GameObject name;


    //database URL
    private string connectionString = "mongodb://silo.cs.indiana.edu:32779/lab10/newDB";

    public string num = "0";

    // Use this for initialization
    void Start() {
        //gets the  vumark manager
		vmm = TrackerManager.Instance.GetStateManager().GetVuMarkManager();

        //connectst to the server & collections
        client = new MongoClient(new MongoUrl(connectionString));
        server = client.GetServer();
        server.Connect();
		db = server.GetDatabase("documents");
        coll = db.GetCollection<Statue>("Other");

        //queries the database
        List<Statue> query = coll.AsQueryable<Statue>().Where<Statue>(u => u.vumark == num).ToList();
        Debug.Log(query);
        foreach (var x in query)
        {
            Debug.Log(x.name);
        }
     
        //whenever a new vumark is registered, call SetVumark
		vb = GameObject.Find ("VuMark").GetComponent<VuMarkBehaviour> ();
		vb.RegisterVuMarkTargetAssignedCallback (new System.Action(this.SetVumark));

    }

	void Update()
	{
		foreach (var bhvr in vmm.GetActiveBehaviours())
		{

			vm = bhvr.VuMarkTarget;

			Debug.Log("Found ID number " + vm.InstanceId);

			foreach (Transform child in gameObject.GetComponentInChildren<Transform>()) {

				if (child.name == vm.InstanceId.ToString ()) {

					child.gameObject.SetActive (true);

				} else {

					child.gameObject.SetActive (false);

				}
			}

		}
	}

	Statue Query(string x)
    {
		return coll.AsQueryable<Statue>().Where<Statue>(u => u.vumark == x).FirstOrDefault();
        //gm.GetComponent<DetectHit>().SetActive(query);
    }

    void SetVumark()
    {
		Statue s = Query (vb.VuMarkTarget.InstanceId.ToString());      //vb.VuMarkTarget.InstanceId.ToString());
		Debug.Log (s.name);
		name.GetComponent<TextMesh> ().text = s.name;
		name.transform.localPosition  = setPos (s.namePos);
		name.transform.localRotation = setRot (s.namePos);
		name.transform.localScale = setScale (s.namePos);
		SetSpecial (s.s);
	}

    //Setting Position
	Vector3 setPos(Trans t){
		return new Vector3(t.x, t.y, t.z);;
	}

    //Setting Rotation
	Quaternion setRot(Trans t){
		var temprot = new Quaternion ();
		temprot.eulerAngles = new Vector3 (t.xRot, t.yRot, t.zRot);
		return temprot;
	}

    //Setting Scale
	Vector3 setScale(Trans t){
		return new Vector3 (t.xScale, t.yScale, t.zScale);
	}

    //Setting Special Item
	void SetSpecial(Special s){
		if (s.name == "Steam") {
			//steam.SetActive (true);
			steam.transform.localPosition = setPos (s.trans);
			steam.transform.localRotation = setRot (s.trans);
			steam.transform.localScale = setScale (s.trans);
		}

	}
}
