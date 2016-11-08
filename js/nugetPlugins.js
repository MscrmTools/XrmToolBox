var XRMTOOLBOX_NUGET_API_QUERY = 'https://api-v2v3search-0.nuget.org/query?q=tags:XrmToolBox';
//var RAPPEN_NUGET_API_DETAIL = 'http://api.nuget.org/v3/registration1/{package}/index.json';

var plugins = new Array();
var currentSortingProperty = "title";
var sortOrder = 1;
var totalCount = 0;
var totalHits;
NugetGetDetails = function () {
	var url = XRMTOOLBOX_NUGET_API_QUERY + (totalCount === 0 ? "" : "&skip=" + totalCount);
	getData(url);
};

function getData(url){
	 $.ajax({
        url: url,
        crossDomain: true,
        dataType: 'jsonp',
        success: function (data) {
			totalCount += data.data.length;
			totalHits = data.totalHits;
			
			success(data.data)
			
			if(totalCount < totalHits){
				NugetGetDetails();
			}
			else{
				getLatestVersionDownloads();
			}
        },
        error: error
    });
}

function success(results){
	for(var i=0; i< results.length; i++){
		var nugetPackage = results[i];
		if(nugetPackage.id === "XrmToolBoxPackage"){
			continue;
		}
		
		if(nugetPackage.title.endsWith(" for XrmToolBox")){
			nugetPackage.title = nugetPackage.title.slice(0, -15);
		}
		
		plugins.push(nugetPackage);
	}
}

function error(xhr, options, error){
	console.dir(xhr);
	console.log("XHR: " + xhr.toString());
	console.log("OPT: " + options);
	console.log("ERR: " + error);
	errorCallback(xhr, options, error);
}

function dynamicSort(property, sortOrder) {
    if(property[0] === "-") {
        sortOrder = -1;
        property = property.substr(1);
    }
    return function (a,b) {
        var result = (a[property] < b[property]) ? -1 : (a[property] > b[property]) ? 1 : 0;
        return result * sortOrder;
    }
}

String.prototype.endsWith = function(suffix) {
    return this.indexOf(suffix, this.length - suffix.length) !== -1;
};

function getLatestVersionDownloads(){
	for(var i=0; i< plugins.length; i++){
		nugetPackage = plugins[i];

		 $.ajax({
			url: "https://api-v2v3search-0.nuget.org/query?q=id:" + nugetPackage.id,
			crossDomain: true,
			dataType: 'jsonp',
			success: function (data) {
				var package = data.data[0];
				var latestVersion = package.versions[package.versions.length - 1];

				for(var j=0;j<plugins.length;j++){
					if(plugins[j].id === package.id){
						plugins[j].downloads = latestVersion.downloads;
						$("#" + plugins[j].id.split(".").join("") + "downloads").text(latestVersion.downloads);
						break;
					}
				}

			},
			error: error
		});
	}
	
	plugins.sort(dynamicSort(currentSortingProperty,sortOrder));
	displayPlugins();
}

function displayPlugins(){
	for(var i=0; i< plugins.length; i++){
		$('#PluginsTable').append('<tr class="data-row">'+
				'<td>'+plugins[i].title+'</td>' +
				'<td>'+plugins[i].version+'</td>' +
				'<td>'+plugins[i].authors+'</td>' +
				'<td>'+plugins[i].description+'</td>' +
				'<td style="text-align:center;"><a href="'+plugins[i].projectUrl+'" target="_blank"><span class="glyphicon glyphicon-globe" aria-hidden="true"></span></a></td>' +
				'<td id="'+plugins[i].id.split(".").join("")+'downloads">'+(plugins[i].downloads ? plugins[i].downloads : 'Loading...')+'</td>' +
				'<td>'+plugins[i].totalDownloads+'</td>' +
				'</tr>');
	}
}

$(document).ready(function() {
		$("TD.sortable").on("click", function(){
			$('TR.data-row').remove();
			
			$(".glyphicon-chevron-up").remove();
			$(".glyphicon-chevron-down").remove();
			
			if(currentSortingProperty === $(this).attr("id")){
				sortOrder = sortOrder === 1 ? -1 : 1;
			}
			else{
				sortOrder = 1;
			}
			$(this).append(' <span class="glyphicon glyphicon-chevron-' + (sortOrder === 1 ? 'up' : 'down') + '" aria-hidden="true"></span>');
			currentSortingProperty = $(this).attr("id");
			getLatestVersionDownloads();
			// plugins.sort(dynamicSort(currentSortingProperty,sortOrder));
			// displayPlugins();
		})
		.css("cursor","pointer");

		NugetGetDetails();	
});