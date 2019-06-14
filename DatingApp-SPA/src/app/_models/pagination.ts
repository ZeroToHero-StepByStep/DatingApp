export interface Pagination {
    currentPage: number;
    itemsPerage: number;
    totalItems: number;
    totalPages: number;
}


export class PaginationResult<T>{
    result: T ;
    pagination: Pagination;
}


