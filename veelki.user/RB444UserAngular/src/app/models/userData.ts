export class UserData
{
    id: number;
    userName : string;
    fullName : string;
    email : string;
    normalizedEmail : string;
    emailConfirmed : boolean;
    phoneNumber : string;
    roleId: number;
    rollingCommission : boolean;
    assignCoin: number;
    commision: number;
    exposureLimit: number;
    parentId: number;
    status: number;

    constructor(private userData : UserData){
        this.id = userData.id;
        this.userName = userData.userName;
        this.fullName = userData.fullName;
        this.email = userData.email;
        this.normalizedEmail = userData.normalizedEmail;
        this.emailConfirmed = userData.emailConfirmed;
        this.phoneNumber = userData.phoneNumber;
        this.roleId = userData.roleId;
        this.rollingCommission = userData.rollingCommission;
        this.assignCoin = userData.assignCoin;
        this.commision = userData.commision;
        this.exposureLimit = userData.exposureLimit;
        this.parentId = userData.parentId;
        this.status = userData.status;
    }

}