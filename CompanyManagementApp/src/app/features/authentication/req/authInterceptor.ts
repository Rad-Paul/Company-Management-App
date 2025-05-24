import {  HttpRequest, HttpHandlerFn } from "@angular/common/http";

export function AuthInterceptor(req: HttpRequest<unknown>, next: HttpHandlerFn){
    const token : string | null = localStorage.getItem('token');

        if(token){
            const reqClone = req.clone({
                setHeaders: {
                    Authorization: `Bearer ${token}`,
                }
            });

            return next(reqClone);
        }

    return next(req);
}