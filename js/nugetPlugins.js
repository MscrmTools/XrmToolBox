var XRMTOOLBOX_NUGET_API_QUERY = 'https://api-v2v3search-0.nuget.org/query?q=tags:XrmToolBox';
//var RAPPEN_NUGET_API_DETAIL = 'http://api.nuget.org/v3/registration1/{package}/index.json';

var plugins = new Array();
var currentSortingProperty = "title";
var sortOrder = 1;

NugetGetDetails = function (successCallback, errorCallback) {
    $.ajax({
        url: XRMTOOLBOX_NUGET_API_QUERY,
        crossDomain: true,
        dataType: 'jsonp',
        success: function (data) {
            if (data && data.data && data.data.length > 0) {
				successCallback(data.data);
			}
        },
        error: function (xhr, options, error) {
            console.dir(xhr);
            console.log("XHR: " + xhr.toString());
            console.log("OPT: " + options);
            console.log("ERR: " + error);
            errorCallback(xhr, options, error);
        }
    });
};

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

function displayPlugins(){
	for(var i=0; i< plugins.length; i++){
		var nugetPackage = plugins[i];

		$('#PluginsTable').append('<tr class="data-row">'+
		'<td>'+nugetPackage.title+'</td>' +
		'<td>'+nugetPackage.version+'</td>' +
		'<td>'+nugetPackage.authors+'</td>' +
		'<td>'+nugetPackage.description+'</td>' +
		'<td style="text-align:center;"><a href="'+nugetPackage.projectUrl+'" target="_blank"><span class="glyphicon glyphicon-globe" aria-hidden="true"></span></a></td>' +
		'<td>'+nugetPackage.totalDownloads+'</td>' +
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
			plugins.sort(dynamicSort(currentSortingProperty,sortOrder));
			displayPlugins();
		})
		.css("cursor","pointer");

		NugetGetDetails(
		function(results){
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
			
			plugins.sort(dynamicSort(currentSortingProperty,sortOrder));
			displayPlugins();
		},
		function(error){
			alert("Erreur");
		});
	
});