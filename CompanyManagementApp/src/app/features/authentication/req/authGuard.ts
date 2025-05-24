import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';
import {  Router } from '@angular/router';

export function Authenticated() : boolean{
    const router : Router = inject(Router);
    const platformId : Object = inject(PLATFORM_ID);

    if(isPlatformBrowser(platformId)){
        const token = localStorage.getItem('token');
        if(token) return true;
    }
    
    router.navigate(['auth/login']);
    return false;
}