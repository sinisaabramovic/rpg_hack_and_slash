using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeSpline : MonoBehaviour {

    // Use this for initialization

    public enum PathStates
    {
        draw,
        destroy,
        idle
    }

    public PathStates pathStates;

    public Camera camera;
    public GameObject objectToDraw;
    public GameObject debugPathObjectDraw;

    public List<Vector3> pathPositions;
    public int checkpointIncrement;
    public int maxNumberOfPath = 10;

	private float drawTime = 0.05f;
	private float drawTrashold = 2.0f;
    private float maxDrawTrashold = 1.5f;
    private float drawTrasholdMaxDistance = 0.5f;
    private List<GameObject> path;
    private List<GameObject> mainPathCalc;
    private bool can_draw = false;
    private float mouse_z_distance = 10.0f;
    private float maxDistanceToDrawPath = 1f;

	void Start () {
        //StartCoroutine(drawPath(drawTime));
        camera = Camera.main;
        path = new List<GameObject>();
        pathPositions = new List<Vector3>();
        mainPathCalc = new List<GameObject>();
        StartCoroutine(DoDraw(drawTime));
	}

    void addPathPoint(Vector3 hitPosition){
        Vector3 drawPathPosistion = hitPosition;
        GameObject goPath = Instantiate(debugPathObjectDraw, drawPathPosistion, Quaternion.identity);
        mainPathCalc.Add(goPath);
        Debug.Log("hit distance = " + hitPosition);
    }

    void DrawPath(float timeRate){

        pathStates = PathStates.draw;
        //Debug.Log("ENTER DRAW");
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out hit))
        {
            Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, mouse_z_distance);
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
            if (path.Count == 0)
            {
                GameObject goP = Instantiate(objectToDraw, mousePosition, Quaternion.identity);

                Ray downRay = new Ray(goP.transform.position, -Vector3.up);

                if (Physics.Raycast(downRay, out hit))
                {
                    Vector3 forward = goP.transform.TransformDirection(-Vector3.up) * hit.distance;
                    Debug.DrawRay(goP.transform.position, forward, Color.green);
                    //Debug.Log("hit distance = " + hit.distance);
                    Vector3 drawPathPosistion = hit.point;
                    drawPathPosistion.y = drawPathPosistion.y + maxDistanceToDrawPath;;
                    addPathPoint(drawPathPosistion);
                }

                path.Add(goP);
            }
            else
            {
                if (Vector3.Distance(path[path.Count - 1].transform.position, mousePosition) > drawTrashold)
                {
                    Vector3 drawPos = mousePosition;
                    Vector3 direction = mousePosition - path[path.Count - 1].transform.position;
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    if (Vector3.Distance(path[path.Count - 1].transform.position, mousePosition) > (drawTrashold + drawTrasholdMaxDistance)){
                        drawPos = path[path.Count - 1].transform.position + direction / 2.0f;

                    }else{
                        drawPos = mousePosition;
                    }

					GameObject goP = Instantiate(objectToDraw, drawPos, Quaternion.identity);

                    Ray downRay = new Ray(goP.transform.position, -Vector3.up);

                    if (Physics.Raycast(downRay, out hit))
                    {
                        Vector3 forward = goP.transform.TransformDirection(-Vector3.up) * hit.distance;
                        Debug.DrawRay(goP.transform.position, forward, Color.green);

                        Vector3 drawPathPosistion = hit.point;
                        drawPathPosistion.y = drawPathPosistion.y + maxDistanceToDrawPath;
                        addPathPoint(drawPathPosistion);

                    }

                    goP.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                    path.Add(goP);
                }
            }
        }           
    }

    private void clearPathPositons(){
        //pathPositions.Clear();
    }

    private void addPathToPathPositions()
    {
        clearPathPositons();
        if(path != null){
            foreach (GameObject go in mainPathCalc)
            {
                pathPositions.Add(go.transform.position);
            } 
        }

    }

    public void destoryPathConditions(){
        if (path != null)
        {
            if (path.Count >= maxNumberOfPath)
            {
                addPathToPathPositions();
                foreach (GameObject go in path)
                {
                    pathStates = PathStates.destroy;
                    Destroy(go);
                }
                path.Clear();

                foreach (GameObject go in mainPathCalc)
                {
                    pathStates = PathStates.destroy;
                    Destroy(go);
                }
                mainPathCalc.Clear();
            } 
        }

    }

    public void destoryPath(){        
        addPathToPathPositions();

        if (path != null){
            foreach (GameObject go in path)
            {
                pathStates = PathStates.destroy;
                Destroy(go);
            }

            foreach (GameObject go in mainPathCalc)
            {
                pathStates = PathStates.destroy;
                Destroy(go);
            }
            mainPathCalc.Clear();

            path.Clear(); 
        }
    }

    IEnumerator DoDraw(float timeRate){
       //Debug.Log("ENTER");
        while (true)
        {
            //Debug.Log("ENTER Corutine");
            if(Input.GetMouseButton(0)){
                DrawPath(drawTime);     
            }else{
                pathStates = PathStates.destroy;
                destoryPath(); 
            }
            pathStates = PathStates.idle;
            destoryPathConditions();
            yield return new WaitForSeconds(timeRate);
        }
    }
}
