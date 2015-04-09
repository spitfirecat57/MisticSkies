#pragma strict

var cubemap : Cubemap;
var currentMaterial : Material;
var updateRate = 1.0;
private var renderFromPosition : Transform;
private var minz = -1.0;
 
function Start () {
    renderFromPosition = transform;
}
 
function Update () {
    if(Time.time - updateRate > minz){
        minz = Time.time - Time.deltaTime;
        RenderMe();
        currentMaterial.SetTexture("_Cube",cubemap);
        renderer.material = currentMaterial;
    }
}
 
function RenderMe(){
    var go = new GameObject( "CubemapCamera"+Random.seed, Camera );
       
    go.camera.backgroundColor = Color.black;
    go.camera.cullingMask = ~(1<<8);
    go.transform.position = renderFromPosition.position;
    if(renderFromPosition.renderer )go.transform.position = renderFromPosition.renderer.bounds.center;
    go.transform.rotation = Quaternion.identity;
   
    go.camera.RenderToCubemap( cubemap );
 
    DestroyImmediate( go );
}