var cam : Transform;

private var nextPos : float;

private var nextPos2 : float;

private var dampVelocity : float;

private var dampVelocity2 : float;




function Update () {

    var newPos = Mathf.SmoothDamp(cam.transform.localPosition.x, nextPos, dampVelocity, 0.2);

    var newPos2 = Mathf.SmoothDamp(cam.transform.localPosition.y, nextPos2, dampVelocity2, 0.2);

    cam.transform.localPosition.x = newPos;

    cam.transform.localPosition.y = newPos2;

    if(Input.GetKey("e")){
        
        //this value is the distance your camere goes right when you peek
        nextPos = 0.25;

        nextPos2 = 0.0;
    
        

    }else if(Input.GetKey("q")){

        //this value is the distance your camere goes left when you peek
        nextPos = -0.25;

        nextPos2 = 0.0;
        

    }else if(Input.GetKey("left ctrl")){  

        nextPos = 0;

        nextPos2 = -0.45;
        

    }else{
        nextPos = 0.0;

        nextPos2 = 0;
        
    }
}