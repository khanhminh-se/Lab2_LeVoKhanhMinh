// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

function handleDeleteDeviceCategory(deviceCategoryId){
   if(confirm("Are you sure you want to delete?")){
       fetch("/DeviceCategory/Delete/" + deviceCategoryId, {
           method: "POST",
       }).then(res => {
           if(res.ok){
               alert("Successfully deleted device category");
               location.reload();
           }else{
               alert("Failed to delete device category");
           }
       }).catch(err=>{
           alert("Failed to delete device category");
       })
   }
}
function handleEditDeviceCategory(deviceCategoryId){
    window.location.href = "/DeviceCategory/Edit?Id=" + deviceCategoryId;
}
function getFile() {
    var inputElement = document.getElementById("imageFile");
    var file = inputElement.files[0];
    var displayImageEle = document.getElementById("show-image");
    console.log(displayImageEle);
    console.log(file);
    if (file) {
        const imageUrl = URL.createObjectURL(file);
        displayImageEle.src = imageUrl;
        displayImageEle.style.display = "block";
    } else {
        console.log("No file selected");
    }
}
