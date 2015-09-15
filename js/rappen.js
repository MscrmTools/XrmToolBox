var RAPPEN_GH_PAGE_SIZE = 100;


UpdateDownloads = function (version, published, currentcount, releaselink) {
    $("#" + version).text("Loading...");
    //$("#" + published).text("Loading...");
    $("#" + currentcount).text("Loading...");
    $.ajax({
        url: 'https://api.github.com/repos/' + GH_USER + '/' + GH_REPO + '/releases/latest',
        success: function (data) {
            if (data && data.assets && data.assets.length > 0) {
                var count = data.assets[0].download_count;
                
                var date = new Date(data.published_at);
                $("#" + version).text(data.tag_name);
                $("#" + currentcount).text(count);
                //$("#latest-download span").text("Download " + tag);
                $("#latest-download").attr('href', data.assets[0].browser_download_url);
                //$("#" + releaselink).attr('href', data.html_url);
                
				var bodyWithBr = data.body.replace(/(\r\n|\n|\r)/g,"<br />");
				var bodyWithoutDashes = bodyWithBr.replace(/(## )/g,"");
				
				$("#release-notes").html(bodyWithoutDashes);
				
            } else {
                $("#" + version).text("");
                //$("#" + published).text("");
                $("#" + currentcount).text("");
                //$("#latest-download span").text("No download available");
                $("#latest-download").attr('href', "#");
            }
        },
        error: function (xhr, options, error) {
            //$("#" + published).text("");
            $("#" + currentcount).text("");
            if (xhr && xhr.status && xhr.status == 403) {
                $("#" + version).text("");
                //if (xhr.responseText) {
                //    var response = JSON.parse(xhr.responseText);
                //    if (response.message) {
                //        $("#latest-version").text(response.message);
                //    }
                //}
                $("#latest-download").text("You really want it, right!?");
                $("#latest-download").attr('href', 'https://github.com/' + GH_USER + '/' + GH_REPO + '/releases');
            }
            else {
                $("#" + version).text(error);
              //  $("#latest-download span").text("");
              //  $("#latest-download").attr('href', "#");
            }
        }
    });
};

UpdateTotalDownloads = function (totalcount) {
    $("#" + totalcount).text("");
    $.ajax({
        url: 'https://api.github.com/repos/' + GH_USER + '/' + GH_REPO + '/releases',
        success: function (data) {
            if (data && data.length > 0) {
                var count = 0;
                $(data).each(function (index) {
                    if (this.prerelease == false && this.assets.length > 0) {
                        count += this.assets[0].download_count;
                    }
                });
                if (GH_REPO == "FetchXMLBuilder") {
                    // Add codeplex count
                    count += 1010;   // Updated 2015-04-15
                }
                $("#" + totalcount).text(count);
            }
        }
    });
};

GetLatestDownloadLink = function () {
    var url = "";
    $.ajax({
        async: false,
        url: 'https://api.github.com/repos/' + GH_USER + '/' + GH_REPO + '/releases/latest',
        success: function (data) {
            if (data && data.assets && data.assets.length > 0) {
                url = data.assets[0].browser_download_url;
            }
        },
        error: function (xhr, options, error) {
            return "/";
        }
    });

    return url;
};

GetLatestVersion = function () {
    var version = "";
    $.ajax({
        async: false,
        url: 'https://api.github.com/repos/' + GH_USER + '/' + GH_REPO + '/releases/latest',
        success: function (data) {
            if (data) {
                version = data.tag_name;
            }
        },
        error: function (xhr, options, error) {
        }
    });
    return version;
};

UpdateReleaseNotes = function (releasenotes, callback) {
    $("#" + releasenotes).text("Loading...");
    $.ajax({
        url: 'https://api.github.com/repos/' + GH_USER + '/' + GH_REPO + '/releases/latest',
        success: function (data) {
            if (data && data.assets && data.assets.length > 0) {
                var notes = data.body;
                var converter = new Showdown.converter();
                var htmlnotes = converter.makeHtml(notes);
                // Correction for github flavor of markdown, issue references
                htmlnotes = htmlnotes.replace(/<h1>/g, '#').replace('</h1>', '');
                htmlnotes = htmlnotes.replace(/<p>/g, '<br/><br/><p>');
                $("#" + releasenotes).html(htmlnotes);
            } else {
                $("#" + releasenotes).text("");
            }
            if (callback) {
                callback();
            }
        },
        error: function (xhr, options, error) {
            $("#" + releasenotes).text("");
            if (xhr && xhr.status && xhr.status == 403) {
                $("#" + releasenotes).text("You really want it, right!?");
            }
            else {
                $("#" + releasenotes).text(error);
            }
        }
    });
};

LoadIssues = function (open, total) {
    $("#" + open).text("?");
    $("#" + total).text("?");
    $.ajax({
        url: 'https://api.github.com/repos/' + GH_USER + '/' + GH_REPO + '/issues?state=open&per_page=' + RAPPEN_GH_PAGE_SIZE,
        success: function (data) {
            var count = 0;
            if (data) {
                if (data.length >= RAPPEN_GH_PAGE_SIZE) {
                    count = RAPPEN_GH_PAGE_SIZE + "+";
                } else {
                    count = data.length;
                }
            }
            $("#" + open).text(count);
        }
    });
    $.ajax({
        url: 'https://api.github.com/repos/' + GH_USER + '/' + GH_REPO + '/issues?state=all&per_page=' + RAPPEN_GH_PAGE_SIZE,
        success: function (data) {
            var count = 0;
            if (data) {
                if (data.length >= RAPPEN_GH_PAGE_SIZE) {
                    count = RAPPEN_GH_PAGE_SIZE + "+";
                } else {
                    count = data.length;
                }
            }
            $("#" + total).text(count);
        }
    });
};

var sort_by = function (field, reverse, primer) {

    var key = primer ?
        function (x) { return primer(x[field]) } :
        function (x) { return x[field] };

    reverse = !reverse ? 1 : -1;

    return function (a, b) {
        return a = key(a), b = key(b), reverse * ((a > b) - (b > a));
    }
};

Date.prototype.toFormattedString = function (format) {
    /// <summary>
    /// Formats date string dd (date), mm (month), yyyy (year), MM (min), hh (hour), ss (seconds), ms (millisec), APM (AM/PM)
    /// </summary>
    /// <param name="format"></param>
    /// <returns type=""></returns>
    var d = this;
    var f = "";
    f = f + format.replace(/dd|mm|yyyy|MM|hh|ss|ms|APM|\s|\/|\-|,|\./ig, function match() {
        switch (arguments[0]) {
            case "dd":
                var dd = d.getDate();
                return (dd < 10) ? "0" + dd : dd;
            case "mm":
                var mm = d.getMonth() + 1;
                return (mm < 10) ? "0" + mm : mm;
            case "yyyy": return d.getFullYear();
            case "hh":
                var hh = d.getHours();
                return (hh < 10) ? "0" + hh : hh;
            case "MM":
                var MM = d.getMinutes();
                return (MM < 10) ? "0" + MM : MM;
            case "ss":
                var ss = d.getSeconds();
                return (ss < 10) ? "0" + ss : ss;
            case "ms": return d.getMilliseconds();
            case "APM":
                var apm = d.getHours();
                return (apm < 12) ? "AM" : "PM";
            default: return arguments[0];
        }
    } // end match function
    ); // end format.replace
    return f;
};

Number.prototype.padLeft = function (n, str) {
    return Array(n - String(this).length + 1).join(str || '0') + this;
};
