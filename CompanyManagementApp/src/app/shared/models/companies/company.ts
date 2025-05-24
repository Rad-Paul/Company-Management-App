export default class Company{
    id : string;
    name: string;
    fullAddress: string;

    constructor(id : string, name : string, fullAddress : string){
        this.id = id;
        this.name = name;
        this.fullAddress = fullAddress;
    }
}