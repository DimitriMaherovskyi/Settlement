(function (angular) {

    angular
        .module("settlementModule")
        .factory("accountsDataService", accountsDataService);

    accountsDataService.$inject = ["$http"];

    function accountsDataService($http) {

        var service = {
            getAccounts: getAccountsAjaxMock,
            changeAccount: changeAccount,
            addAccount: addAccount,
            getRoles: getRolesMock
        };

        return service;

        function changeAccount(userId, newValue) {
            var promise = $http({
                url: '/QuotesReview/ChangeQuote',
                method: "GET",
                params: { userId: userId, newValue: newValue}
            });
            return promise;
        }

        function getAccountsAjax() {
            var promise = $http.get('/QuotesReview/GetQuotes');
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
                url: '/Accounts/GetRoles',
                method: "GET",
            });
            return promise;
        }

        function addAccount() {

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