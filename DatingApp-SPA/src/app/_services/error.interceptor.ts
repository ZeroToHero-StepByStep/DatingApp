import { Injectable } from "@angular/core";
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from "@angular/common/http";
import { Observable, throwError } from "rxjs";
import { catchError } from "rxjs/operators";


@Injectable()
export class ErrorInterceptor implements HttpInterceptor{

    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>>{
            return next.handle(req).pipe(
                catchError(
                    error =>{
                        if ( error instanceof HttpErrorResponse){
                            if(error.status == 401){
                                return throwError(error.statusText);
                            }
                            const applicatoinError = error.headers.get('Application-Error');
                            if(applicatoinError){
                                console.error(applicatoinError);
                                return throwError(applicatoinError);
                            }
                            const serverError = error.error;
                            let modalStateError = '';
                            if (serverError && typeof serverError === 'object'){
                                for ( const key in serverError){
                                    modalStateError += serverError[key] + '\n';
                                }
                            }
                            return throwError(modalStateError || serverError || 'Server Error');
                        }
                    })
                );
    }
}


export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true

};