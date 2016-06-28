(function (angular) {

    angular
        .module("settlementModule")
        .factory("accountsDataService", accountsDataService);

    accountsDataService.$inject = ["$http"];

    function accountsDataService($http) {

        var service = {
            getAccounts: getAccountsAjax,
            changeAccount: changeAccount,
            addAccount: addAccount,
            getRoles: getRolesAjax
        };

        return service;

        function changeAccount(changedAccount) {
            var promise = $http({
                url: '/QuotesReview/ChangeQuote',
                method: "POST",
                params: {
                    UserId: changedAccount.UserId,
                    Username: changedAccount.UserName,
                    Email: changedAccount.Email,
                    RoleId: changedAccount.RoleId,
                    Quote: changedAccount.Quote,
                    FirstName: changedAccount.FirstName,
                    LastName: changedAccount.LastName,
                }
            });
            return promise;
        }

        function getAccountsAjax() {
            var promise = $http.get('/SystemUsers/GetUsers');
            return promise;
        }

        function getRolesAjax() {
            var promise = $http.get('/SystemUsers/GetRoles');
            return promise;
        }

        function getAccountsAjaxMock() {
            return [
                {
                    UserId: 1,
                    UserName: 'johnSmith',
                    Email: 'johnSmith@mail.com',
                    CreatedDate: '15.05.2016',
                    LastLoginDate: '12.06.2016',
                    RoleId: 1,
                    Quote: 300,
                    FirstName: 'John',
                    LastName: 'Smith',
                },
                {
                    UserId: 2,
                    UserName: 'alexSmith',
                    Email: 'johnSmith@mail.com',
                    CreatedDate: '18.05.2016',
                    LastLoginDate: '25.06.2016',
                    RoleId: 2,
                    Quote: 300,
                    FirstName: 'John',
                    LastName: 'Smith',
                },
                {
                    UserId: 3,
                    UserName: 'kateSmith',
                    Email: 'johnSmith@mail.com',
                    CreatedDate: '05.05.2016',
                    LastLoginDate: '22.06.2016',
                    RoleId: 3,
                    Quote: 300,
                    FirstName: 'John',
                    LastName: 'Smith',
                }
            ]
        }

        function getRoles() {
            var promise = $http({
                url: '/Account/GetRoles',
                method: "GET",
            });
            return promise;
        }

        function addAccount(newAccount) {
            var promise = $http({
                url: '/SystemUsers/AddUser',
                method: "POST",
                params: {
                    Username: newAccount.UserName,
                    Email: newAccount.Email,
                    RoleId: newAccount.RoleId,
                    Quote: newAccount.Quote,
                    FirstName: newAccount.FirstName,
                    LastName: newAccount.LastName,
                    Password: newAccount.Password
                }
            });
            return promise;
        }

        function deleteAccount(userId) {
            var promise = $http({
                url: '/SystemUsers/DeleteUser',
                method: "POST",
                params: {
                    UserId: userId,
                }
            });
            return promise;
        }

        function getRolesMock() {
            return [
                {
                    RoleId: 1,
                    RoleName: 'Admin'
                },
                {
                    RoleId: 2,
                    RoleName: 'Warden'
                },
                {
                    RoleId: 3,
                    RoleName: 'Dean'
                }
            ];
        }
    }

})(angular);