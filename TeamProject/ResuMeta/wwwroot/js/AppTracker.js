document.addEventListener("DOMContentLoaded", initializePage, false);

function initializePage() {
    refreshTable();
    const applicationForm = document.getElementById('application-form');
    applicationForm.addEventListener('submit', addApplication, false);
    const sortButton = document.getElementById('apply-filters');
    sortButton.addEventListener('click', sortTable, false);
    const resetButton = document.getElementById('reset-filters');
    resetButton.addEventListener('click', refreshTable, false);
}

function setApplyReminder(applicationTrackerId) {
    //console.log("Setting reminder to apply");
    const userId = document.getElementById('userId').value;

    fetch(`/api/sendgrid/apply`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            userId: userId,
            applicationTrackerId: applicationTrackerId
        })
    })
        .then(response => {
            if (response.ok) {
                alert("Reminder set successfully! You will recieve an email 2 days before the application deadline.");
            } else {
                alert("Sorry, something went wrong. Please try again.");
            }
        })
        .catch(error => {
            //console.error('Error:', error);
            console.log("error");
            alert("Failed to set reminder. Please try again.");
        });
}

function setFollowupReminder(applicationTrackerId) {
    //console.log("Setting reminder to follow up");
    const userId = document.getElementById('userId').value;

    fetch(`/api/sendgrid/followup`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            userId: userId,
            applicationTrackerId: applicationTrackerId
        })
    })
        .then(response => {
            if (response.ok) {
                alert("Follow up Reminder set successfully! You will recieve an email 7 days after the apply date.");
            } else {
                alert("Sorry, something went wrong. Please try again.");
            }
        })
        .catch(error => {
            console.log("error");
            alert("Failed to set follow up reminder. Please try again.");
        });
}

function addApplication(event) {
    event.preventDefault();
    //console.log("Adding application tracker");
    const userInfoId = document.getElementById('userId');
    const jobTitle = document.getElementById('jobTitle');
    const companyName = document.getElementById('companyName');
    const jobListingUrl = document.getElementById('jobListingUrl');
    const appliedDate = document.getElementById('appliedDate');
    const applicationDeadline = document.getElementById('applicationDeadline');
    const status = document.getElementById('status');
    const notes = document.getElementById('notes');

    const applicationTracker = {
        userInfoId: parseInt(userInfoId.value, 10),
        jobTitle: jobTitle.value,
        companyName: companyName.value,
        jobListingUrl: encodeURIComponent(jobListingUrl.value),
        appliedDate: appliedDate.value,
        applicationDeadline: applicationDeadline.value,
        status: status.value,
        notes: notes.value

    };
    if (appliedDate.value) {
        applicationTracker.appliedDate = appliedDate.value;
    }

    if (applicationDeadline.value) {
        applicationTracker.applicationDeadline = applicationDeadline.value;
    }
    // console.log(applicationTracker);
    fetch('/api/applicationtracker/applicationInfo', {
        method: 'POST',
        headers: {
            'Accept': 'application/json; application/problem+json; charset=utf-8',
            'Content-Type': 'application/json; charset=UTF-8'
        },
        body: JSON.stringify(applicationTracker)
    })
        .then(response => {
            if (response.ok) {
                //window.alert("Application tracker added successfully");
                refreshTable();
            }
            else {
                window.alert("Uh oh. Something went wrong while trying to add this app tracker.  Please try again.");
            }
        })
        .catch(error => console.error('Error:', error));

}

function deleteApplication(applicationInfoId) {
    fetch(`/api/applicationtracker/${applicationInfoId}`, {
        method: 'DELETE',
    })
        .then(response => {
            if (response.ok) {
                //window.alert("Application tracker deleted successfully");
                refreshTable();
            }
            else {
                window.alert("Uh oh. Something went wrong while trying to delete this app tracker.  Please try again.");
            }
        })
        .catch(error => console.error('Error:', error));
}

function refreshTable(sortOption, sortOrder) {
    const userId = document.getElementById('userId').value;
    // console.log(userId);
    fetch(`/api/applicationtracker/${userId}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            if (sortOption && sortOrder) {
                data.sort(function (a, b) {
                    var dateA, dateB;

                    if (sortOption === 'applied-date') {
                        dateA = new Date(a.appliedDate);
                        dateB = new Date(b.appliedDate);
                    } else if (sortOption === 'application-deadline') {
                        dateA = new Date(a.applicationDeadline);
                        dateB = new Date(b.applicationDeadline);
                    }

                    if (sortOrder === 'ascending') {
                        return dateA - dateB;
                    } else if (sortOrder === 'descending') {
                        return dateB - dateA;
                    }
                });
            }
            const appTable = document.getElementById('app-table');

            while (appTable.rows.length > 1) {
                appTable.deleteRow(1);
            }

            if (data.length === 0) {
                let newRow = appTable.insertRow(-1);
                let cell = newRow.insertCell(0);
                cell.innerHTML = "No application trackers to display";
                cell.colSpan = 8;
            } else {
                data.forEach(item => {
                    let newRow = appTable.insertRow(-1);
                    let cell1 = newRow.insertCell(0);
                    let cell2 = newRow.insertCell(1);
                    let cell3 = newRow.insertCell(2);
                    let cell4 = newRow.insertCell(3);
                    let cell5 = newRow.insertCell(4);
                    let cell6 = newRow.insertCell(5);
                    let cell7 = newRow.insertCell(6);
                    //let cell8 = newRow.insertCell(7);
                    let url = decodeURIComponent(item.jobListingUrl);

                    cell1.innerHTML = item.jobTitle;
                    cell1.className = 'wrap-text';
                    cell2.innerHTML = item.companyName;
                    cell2.className = 'wrap-text';
                    cell3.innerHTML = `<a href="${url}" target="_blank">Click Here</a>`;
                    cell3.className = 'wrap-text';
                    cell4.innerHTML = item.appliedDate;
                    cell4.className = 'wrap-text';
                    cell5.innerHTML = item.applicationDeadline;
                    cell5.className = 'wrap-text';
                    cell6.innerHTML = item.status;
                    cell6.className = 'wrap-text';
                    cell7.innerHTML = item.notes;
                    cell7.className = 'wrap-text';
                    const deleteCell = newRow.insertCell();
                    const deleteButton = document.createElement('button');
                    deleteButton.innerHTML = '<i class="fa fa-trash"></i>';
                    deleteButton.addEventListener('click', function () {
                        event.preventDefault();
                        deleteApplication(item.applicationTrackerId);
                    });
                    deleteCell.appendChild(deleteButton);

                   const reminderCell = newRow.insertCell();
                    const reminderDropdown = document.createElement('select');
                    const defaultOption = document.createElement('option');
                    defaultOption.text = 'Reminder?';
                    defaultOption.value = '';
                    reminderDropdown.add(defaultOption);

                    const applyReminderOption = document.createElement('option');
                    applyReminderOption.text = 'Set Apply Reminder';
                    applyReminderOption.value = 'apply';
                    reminderDropdown.add(applyReminderOption);

                    const followupReminderOption = document.createElement('option');
                    followupReminderOption.text = 'Set Followup Reminder';
                    followupReminderOption.value = 'followup';
                    reminderDropdown.add(followupReminderOption);

                    reminderDropdown.addEventListener('change', function () {
                        event.preventDefault();
                        if (this.value === 'apply') {
                            setApplyReminder(item.applicationTrackerId);
                            applyReminderOption.text = 'Application Reminder scheduled';
                            applyReminderOption.disabled = true;
                        } else if (this.value === 'followup') {
                            setFollowupReminder(item.applicationTrackerId);
                            followupReminderOption.text = 'Follow Up Reminder scheduled';
                            followupReminderOption.disabled = true;
                        }
                    });

                    reminderCell.appendChild(reminderDropdown);
                });
            }
        })
        .catch(error => console.error('Error:', error));
}

function sortTable() {
    var sortOption = document.getElementById('sort-option').value;
    var sortOrder = document.getElementById('sort-order').value;
    refreshTable(sortOption, sortOrder);
}