<?php
// required headers
header("Access-Control-Allow-Origin: *");
header("Content-Type: application/json; charset=UTF-8");
 
// include database and object files
include_once '../config/database.php';
include_once '../objects/playgroup.php';
 
// instantiate database and playgroup object
$database = new Database();
$db = $database->getConnection();
 
// initialize object
$playgroup = new Playgroup($db);
 
// query playgroups
$stmt = $playgroup->read();
$num = $stmt->rowCount();
 
// check if more than 0 record found
if($num > 0){
 
    // playgroups array
    $playgroups_arr=array();
    $playgroups_arr["records"]=array();
 
    // retrieve our table contents
    // fetch() is faster than fetchAll()
    // http://stackoverflow.com/questions/2770630/pdofetchall-vs-pdofetch-in-a-loop
    while ($row = $stmt->fetch(PDO::FETCH_ASSOC)){
        // extract row
        // this will make $row['name'] to
        // just $name only
        extract($row);
 
        $playgroup_item=array(
            "Id" => $Id,
            "Name" => $Name,
        );
 
        array_push($playgroups_arr["records"], $playgroup_item);
    }
 
    echo json_encode($playgroups_arr);
}
 
else{
    echo json_encode(
        array("message" => "No playgroups found.")
    );
}
?>