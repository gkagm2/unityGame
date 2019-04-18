using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBase : MonoBehaviour {
    public Material mat;
    void Awake()
    {
        
        //그리기 위해선 MeshFilter 필수
        gameObject.AddComponent<MeshFilter>();

        //랜더링하기
        gameObject.AddComponent<MeshRenderer>();

        //
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        //Size 조정
        mesh.vertices = new Vector3[]
        {
            new Vector3(0.0f,0.0f,0.0f), new Vector3(0.0f,0.3f,0.0f),
            new Vector3(0.3f,0.3f,0.0f), new Vector3(0.3f,0.0f,0.0f)
        };
        // 메쉬의 기본 텍스쳐 좌표를 나타낸다.
        mesh.uv = new Vector2[]
        {
            new Vector2(0.0f,0.0f), new Vector2(0.0f,1.0f),
            new Vector2(1.0f,1.0f), new Vector2(1.0f,0.0f),
        };

        //메쉬의 모든 삼각면(triangle)정보를 가지고 있는 배열을 나타냅니다.
        //배열은 정점 배열의 정보를 가지고 있는, 삼각면의 목록을 나타냅니다. 
        //삼각면 배열의 크기는 반드시 3의 배수여야 합니다.정점(vertices)은 간단히 같은 정점으로 연동해서, 
        //공유될 수 있습니다.메쉬가 여러개의 서브메쉬를(재질) 가진경우에, 삼각면 목록은 모든 서브메쉬의 
        //삼각면 정보를 갖게 됩니다. 삼각면 배열을 할당할 때, subMeshCount 는 1로 설정됩니다. 여러개의 서브메쉬를 
        //갖고 있는 경우에, subMeshCount 와 SetTriangles 를 사용합니다.
        // [ 1  2 ] 위치로  삼각 메쉬를 그리는 듯.
        // [ 0  3 ]
        mesh.triangles = new int[] { 0,1,2,    0,2,3};

        if (mat != null)
        {
            gameObject.GetComponent<Renderer>().material = mat;
        }
        //Recalculates the normals of the Mesh from the triangles and vertices.
        mesh.RecalculateNormals();

        //Recalculate the bounding volume of the Mesh from the vertices.
        mesh.RecalculateBounds();

        //각 재질 안에 있는 텍스처 이미지 크기만큼 오브젝트의 스케일을 재조정하는 함수.
        MakeResizeByImageSize();
        
    }
    void MakeResizeByImageSize()
    {
        Debug.Log("Screen height : "+ Screen.height + Screen.width);

        float one_unit = 2.0f / Screen.height;

        Debug.Log("mat.mainTexture.width : " + mat.mainTexture.width + " mat.aminTeuxture.height : " + mat.mainTexture.height);
        float x = one_unit * mat.mainTexture.width;
        float y = one_unit * mat.mainTexture.height;
        Debug.Log("one_unit : " + one_unit);
        Vector3 newScale = new Vector3(x, y, one_unit);
        transform.localScale = newScale;
    }


    // Update is called once per frame
    void Update () {
		
	}
}
