export default class QueryParameters{
    pageNumber : number;
    pageSize : number;
    orderBy : string;
    searchTerm : string = '';
    constructor(pageNumber: number, pageSize : number, orderBy: string, searchTerm: string | null){
        this.pageNumber = pageNumber;
        this.pageSize = pageSize;
        this.orderBy = orderBy;
        if(!searchTerm)
            searchTerm = '';
        else
            this.searchTerm = searchTerm;
    }
}