document.addEventListener("DOMContentLoaded", function () {
    document.querySelector('.blank-profile-container').style.display = 'block';
    hideAllOtherContainers();

    document.querySelectorAll(".user-list-table tbody tr").forEach(row => {
        row.addEventListener("click", function () {
            showUserDetails(row);
        });
    });

    document.querySelectorAll('.dropdown-item').forEach(item => {
        item.addEventListener('click', function (event) {
            if (this.textContent === 'Edit') {
                event.stopPropagation();

                const selectedRow = this.closest('tr');
                showEditUserDetails(selectedRow);

                const dropdownContent = this.closest('.dropdown-content');
                dropdownContent.classList.remove('show');
            }
        });
    });

});

function hideAllContainers() {
    document.querySelector('.blank-profile-container').style.display = 'none';
    document.querySelector('.selected-profile-container').style.display = 'none';
    document.querySelector('.edit-profile-container').style.display = 'none';
    document.querySelector('.select-org-user-container').style.display = 'none';
    document.querySelector('.edit-org-user-container').style.display = 'none';
    document.querySelector('.select-food-user-container').style.display = 'none';
    document.querySelector('.edit-food-user-container').style.display = 'none';

}
function hideAllOtherContainers() {
    document.querySelector('.selected-profile-container').style.display = 'none';
    document.querySelector('.edit-profile-container').style.display = 'none';
    document.querySelector('.select-org-user-container').style.display = 'none';
    document.querySelector('.edit-org-user-container').style.display = 'none';
    document.querySelector('.select-food-user-container').style.display = 'none';
    document.querySelector('.edit-food-user-container').style.display = 'none';
}

function showUserDetails(row) {
    hideAllContainers();

    const accountType = row.dataset.accounttype;

    if (accountType === 'donor' || accountType === 'admin') {
        document.querySelector('.selected-profile-container').style.display = 'block';

        document.getElementById('firstName').value = row.dataset.firstname;
        document.getElementById('lastName').value = row.dataset.lastname;
        document.getElementById('gender').value = row.dataset.gender;
        document.getElementById('birthDate').value = row.dataset.birthdate;
        document.getElementById('address').value = row.dataset.address;
        document.getElementById('userImg').src = row.dataset.userimg;

    } else if (accountType === 'donee organization') {
        document.querySelector('.select-org-user-container').style.display = 'block';
        document.getElementById('orgName').value = row.dataset.orgname;
        document.getElementById('orgAddress').value = row.dataset.address;
    } else if (accountType === 'food business') {
        document.querySelector('.select-food-user-container').style.display = 'block';

        document.getElementById('foodName').value = row.dataset.foodbussiness;
        document.getElementById('foodAddress').value = row.dataset.address;
    }
}

function showEditUserDetails(row) {
    hideAllContainers();

    const accountType = row.dataset.accounttype;

    if (accountType === 'donor' || accountType === 'admin') {
        document.querySelector('.edit-profile-container').style.display = 'block';

        document.getElementById('editFirstName').value = row.dataset.firstname;
        document.getElementById('editLastName').value = row.dataset.lastname;
        document.getElementById('editGender').value = row.dataset.gender;
        document.getElementById('editBirthDate').value = row.dataset.birthdate;
        document.getElementById('editStreet').value = row.dataset.street;
        document.getElementById('editBaranggay').value = row.dataset.baranggay;
        document.getElementById('editCity').value = row.dataset.city;
        document.getElementById('editProvince').value = row.dataset.province;

    } else if (accountType === 'donee organization') {
        document.querySelector('.edit-org-user-container').style.display = 'block';

        document.getElementById('editOrgName').value = row.dataset.orgname;
        document.getElementById('editStreet').value = row.dataset.street;
        document.getElementById('editBaranggay').value = row.dataset.baranggay;
        document.getElementById('editCity').value = row.dataset.city;
        document.getElementById('editProvince').value = row.dataset.province;

    } else if (accountType === 'food business') {
        document.querySelector('.edit-food-user-container').style.display = 'block';

        document.getElementById('editFoodName').value = row.dataset.foodbussiness;
        document.getElementById('editStreet').value = row.dataset.street;
        document.getElementById('editBaranggay').value = row.dataset.baranggay;
        document.getElementById('editCity').value = row.dataset.city;
        document.getElementById('editProvince').value = row.dataset.province;
    }
}



