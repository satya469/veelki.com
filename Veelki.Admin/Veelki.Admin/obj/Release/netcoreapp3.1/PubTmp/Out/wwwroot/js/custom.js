const defaultsObj = {
    apiUrl: "http://192.168.1.102/Avjet.Web/api/",
    //apiUrl: "http://project.explorateglobal.com/Avjet/",
    resetForm: function (data = {}) {
        for (let [key, value] of Object.entries(data)) {
            switch (key) {
                case "input":
                    value.split(",").map(x => $("#" + x.trim()).val("").attr("readonly", false))
                    break;
                case "select":
                    value.split(",").map(x => $("#" + x.trim()).val(0))
                    break;
                case "checkbox":
                    value.split(",").map(x => $("#" + x.trim()).prop("checked", 0))
                    break;
                case "radio":
                    value.split(",").map(x => $("#" + x.trim()).click())
                    break;
                default:
                    break;
            }

        }
    },
    deleteData: function (methodUrl, deleteId) {
        let result;
        let deleteJson = {
            apiUrl: `${defaultsObj.apiUrl + methodUrl}=${deleteId}`,
            methodType: "GET",
        }
        try {
            result = promiseAjaxGet.call(deleteJson);
        } catch (err) {
            messagePopup.error(err.statusText);
        } finally {
            return result;
        }
    },
    addData: function (methodUrl, formData) {
        let result;
        let addJson = {
            apiUrl: `${defaultsObj.apiUrl + methodUrl}`,
            methodType: "POST",
            postData: JSON.stringify(formData)
        }
        try {
            result = promiseAjaxPost.call(addJson);
        } catch (err) {
            messagePopup.error(err.statusText);
        } finally {
            return result;
        }
    },
    addImage: function (methodUrl, formData) {
        let result;
        let addJson = {
            apiUrl: `${defaultsObj.apiUrl + methodUrl}`,
            methodType: "POST",
            postData: formData
        }
        try {
            result = promiseAjaxPost.call(addJson, false);
        } catch (err) {
            messagePopup.error(err.statusText);
        } finally {
            return result;
        }
    },
    getDataList: async (methodUrl, getType = null) => {
        let resultData;
        let getJson = {
            apiUrl: `${defaultsObj.apiUrl + methodUrl}`,
            methodType: "GET",
        }
        if (getType != null) {
            Object.assign(getJson, { apiUrl: `${defaultsObj.apiUrl + methodUrl}?${getType}` })
        }
        try {
            resultData = await promiseAjaxGet.call(getJson);
        } catch (err) {
            messagePopup.error(err.statusText);
        } finally {
            return resultData;
        }
    },
    getDataDetail: function (data = [], field = null, value = 0) {
        let selectedData = [];
        if (data.length == 0) {
            messagePopup.responseSuccess("No record found.");
        }
        if (field != null) {
            selectedData = data.filter(f => {
                return f[field] == value;
            });
        }
        return selectedData;
    }
}

const messagePopup = {
    success: function (mTitle) {
        swal({
            title: mTitle.toUpperCase(),
            icon: "success",
        });
    },
    error: function (mTitle) {
        swal({
            title: mTitle.toUpperCase(),
            icon: "error",
        });
    },
    responseSuccess: function (mText) {
        return swal({
            text: mText,
            title: "SUCCESS",
            icon: "success",
        });
    },
    responseWarning: function (mText) {
        return swal({
            text: mText,
            title: "Warning",
            icon: "warning",
        });
    },
    responseError: function (mText) {
        swal({
            title: "Error",
            text: mText,
            icon: "error",
        });
    },
    confirmDialog: function (cTitle, cText) {
        return swal({
            title: cTitle,
            text: cText,
            icon: "warning",
            buttons: true,
            dangerMode: true,
        });
    }
}


function promiseAjaxGet() {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: this.apiUrl,
            type: "GET",
            data: this.postData,
            contentType: "application/json; charset=utf-8",
            dataType: this.dataType || 'json',
            cors: true,
            cache: false,
            async: (typeof this.async == 'boolean') ? this.async : true,
            headers: {
                'Access-Control-Allow-Origin': '*',
            },
            beforeSend: function () {
                $(".loader").show();
            },
            success: function (data) {
                if (typeof data == 'string' && this.dataType != 'html') {
                    data = JSON.parse(data);
                }
                resolve(data)
            },
            error: function (err) {
                reject(err)
            },
            complete: function () {
                $(".loader").hide();
            }
        })
    })
};


function promiseAjaxPost(ct = "application/json; charset=utf-8") {
    return new Promise((resolve, reject) => {
        $.ajax({
            url: this.apiUrl,
            type: "POST",
            data: this.postData,
            contentType: ct,
            processData: false,
            dataType: this.dataType || 'json',
            cors: true,
            cache: false,
            async: (typeof this.async == 'boolean') ? this.async : true,
            headers: {
                'Access-Control-Allow-Origin': '*',
            },
            beforeSend: function () {
                $(".loader").show();
            },
            success: function (data) {
                if (this.dataType != "html" && typeof data != 'object') {
                    data = JSON.parse(data);
                }
                resolve(data)
            },
            error: function (err) {
                reject(err)
            },
            complete: function () {
                $(".loader").hide();
            }
        })
    })
};