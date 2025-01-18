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
function handleViewDevicesByCategoryId(){
    const deviceCategoryId = document.getElementById("category-selection").value;
    window.location.href = "/DeviceCategory/DevicesByCategory?deviceCategoryId=" + deviceCategoryId;
}
function handleDeleteDevice(deviceId){
    if(confirm("Are you sure you want to delete?")){
        fetch("Device/Delete?Id=" + deviceId, {
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
function handleEditDevice(deviceId){
    window.location.href = "/Device/Edit?deviceId=" + deviceId;
}
function handleOnChangeFilterOption() {
    const filterOption = document.getElementById("filter-option");
    const filterValue = document.getElementById("filter-value");
    const categoryOptions = document.getElementById("category-options");
    const statusOptions = document.getElementById("status-options");
    const filterItems = document.getElementById("filter-option-items");

    if (filterOption.value !== 'default') {
        filterValue.innerText = "With " + filterOption.value + " :";
        filterItems.style.display = "block";

        if (filterOption.value === "Category") {
            categoryOptions.style.display = "block";
            statusOptions.style.display = "none";
        } else if (filterOption.value === "Status") {
            categoryOptions.style.display = "none";
            statusOptions.style.display = "block";
        }
    } else {
        filterValue.innerText = "";
        filterItems.style.display = "none";
    }
}
function handleOnChangeFilterOptionItems() {
    const filterOption = document.getElementById("filter-option");
    const filterBy = filterOption.value;
    let filterValueSelected = '';

    if (filterBy === 'Category') {
        const category = document.getElementById('category-options').value;
        filterValueSelected = category;
    } else if (filterBy === 'Status') {
        const status = document.getElementById('status-options').value;
        filterValueSelected = status;
    }
    if(filterValueSelected !== 'default') {
        window.location.href=`/Device/FilterDevices?filterBy=${filterBy}&filterValue=${filterValueSelected}`;
    }
    
}
function handleOnChangeSearch(){
    const searchValue = document.getElementById("search-bar").value;
    window.location.href = "/Device/Search?searchTerm=" + searchValue;
}
