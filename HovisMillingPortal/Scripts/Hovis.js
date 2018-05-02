function UserRolesClick(s, e, url, url1, url2, url3) {
    var key = s.GetRowKey(e.visibleIndex);
    var destUrl = "";
    if (e.buttonID === "btnEditRoles") {
        destUrl = url + "?id=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnDelUser") {
        destUrl = url1 + "?id=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnSiteUser") {
        destUrl = url2 + "?id=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnPlantUser") {
        destUrl = url3 + "?id=" + key;
        window.location.href = destUrl;
    }
}

$(document).ready(function () {
    $("#btShowCustomizationWindow").click(
        function () {
            UpdateCustomizationWindowVisibility();
        }
    );
});

function BacsCustomerGrid_CustomizationWindowCloseUp(s, e) {
    UpdateButtonText();
}
function UpdateCustomizationWindowVisibility() {
    if (BacsCustomerGrid.IsCustomizationWindowVisible())
        BacsCustomerGrid.HideCustomizationWindow();
    else
        BacsCustomerGrid.ShowCustomizationWindow();
    UpdateButtonText();
}
function UpdateButtonText() {
    var text = BacsCustomerGrid.IsCustomizationWindowVisible() ? "Hide" : "Show";
    text += " Customization Window";
    $("#btShowCustomizationWindow").val(text);
}

$(document).ready(function () {
    $("#btShowCustomFilterWindow").click(
        function () {
            BacsCustomerGrid.ShowFilterControl();
        }
    );
});

function BacsCustomerbtn(s, e, url) {
    var key = s.GetRowKey(e.visibleIndex);
    if (e.buttonID === "btnEditCust") {
        var destUrl = url + "?CustomerRecid=" + key;
        window.location.href = destUrl;
    }
}

function BacsHeaderbtn(s, e, url, url1, url2, url3, url4, url5, url6, url7) {
    var key = s.GetRowKey(e.visibleIndex);
    var destUrl = "";
    if (e.buttonID === "btnEditHeader") {
        destUrl = url + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnAuthorise") {
        destUrl = url1 + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnTreasury") {
        destUrl = url2 + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnTreasuryReject") {
        destUrl = url3 + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnView") {
        destUrl = url4 + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnFinance") {
        destUrl = url5 + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnFinanceReject") {
        destUrl = url6 + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnReport") {
        destUrl = url7 + "?headerrecid=" + key;
        window.location.href = destUrl;
    }
}

$(document).ready(function () {
    $("#btBacsHeaderShowCustomizationWindow").click(
        function () {
            UpdateBHCustomizationWindowVisibility();
        }
    );
});

function BacsHeaderGrid_CustomizationWindowCloseUp(s, e) {
    UpdateBHButtonText();
}
function UpdateBHCustomizationWindowVisibility() {
    if (BacsHeaderGrid.IsCustomizationWindowVisible())
        BacsHeaderGrid.HideCustomizationWindow();
    else
        BacsHeaderGrid.ShowCustomizationWindow();
    UpdateBHButtonText();
}
function UpdateBHButtonText() {
    var text = BacsHeaderGrid.IsCustomizationWindowVisible() ? "Hide" : "Show";
    text += " Customization Window";
    $("#btBacsHeaderShowCustomizationWindow").val(text);
}

$(document).ready(function () {
    $("#btBacsHeaderShowCustomFilterWindow").click(
        function () {
            BacsHeaderGrid.ShowFilterControl();
        }
    );
});

function TestHeaderbtn(s, e, url, url1) {
    var key = s.GetRowKey(e.visibleIndex);
    var destUrl = "";
    if (e.buttonID === "btnViewDetail") {
        destUrl = url + "?resultheaderrecid=" + key;
        window.location.href = destUrl;
    }
    if (e.buttonID === "btnGenGetKey") {
        destUrl = url1 + "?resultheaderrecid=" + key;
        window.location.href = destUrl;
    }
}

function DetailsCallbackPanel_BeginCallback(s, e) {
    e.customArgs["resultheaderrecid"] = resultheaderrecid;
}

$(document).ready(function () {
    $("#btShowCustomFilterTestD").click(
        function () {
            TestDetailGridView.ShowFilterControl();
        }
    );
});

$(document).ready(function () {
    $("#btShowCustomizationTestD").click(
        function () {
            UpdateCustomizationWindowVisTestD();
        }
    );
});
function TestDetailGridView_CustomizationWindowCloseUp(s, e) {
    TestDButtonText();
}
function UpdateCustomizationWindowVisTestD() {
    if (TestDetailGridView.IsCustomizationWindowVisible())
        TestDetailGridView.HideCustomizationWindow();
    else
        TestDetailGridView.ShowCustomizationWindow();
    TestDButtonText();
}
function TestDButtonText() {
    var text = TestDetailGridView.IsCustomizationWindowVisible() ? "Hide" : "Show";
    text += " Customization Window";
    $("#btShowCustomizationTestD").val(text);
}

var SiteAuthselectedIDs;
//var SelectedUserName;

function SiteAuthOnBeginCallback(s, e) {
    //Pass all selected keys to GridView callback action
    e.customArgs["SiteAuthselectedIDs"] = SiteAuthselectedIDs;
    //e.customArgs["SelectedUserName"] = SelectedUserName;
}

function SiteAuthonSubmitClick(s, e) {
    //Write all selected keys to hidden input. Pass them on post action.
    $("#SiteAuthselectedIDsHF").val(SiteAuthselectedIDs);
    //$("#SelectedUserName").val(SelectedUserName);
}

$(document).ready(function () {
    $("#btShowCustomFilterUMQCHeader").click(
        function () {
            TestHeaderGrid.ShowFilterControl();
        }
    );
});

$(document).ready(function () {
    $("#btShowCustomizationUMQCHeader").click(
        function () {
            UpdateCustomizationWindowUMQCHeader();
        }
    );
});
function TestHeaderGrid_CustomizationWindowCloseUp(s, e) {
    UMQCHeaderButtonText();
}
function UpdateCustomizationWindowUMQCHeader() {
    if (TestHeaderGrid.IsCustomizationWindowVisible())
        TestHeaderGrid.HideCustomizationWindow();
    else
        TestHeaderGrid.ShowCustomizationWindow();
    UMQCHeaderButtonText();
}
function UMQCHeaderButtonText() {
    var text = TestHeaderGrid.IsCustomizationWindowVisible() ? "Hide" : "Show";
    text += " Customization Window";
    $("#btShowCustomizationUMQCHeader").val(text);
}


function OnSelectedProcessChanged() {
    UMQCHeaderformLayout.GetEditor("ResultPlantRecid").PerformCallback();
}


