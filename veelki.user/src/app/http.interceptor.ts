import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpResponse, HttpRequest, HttpHandler } from '@angular/common/http';
import { Observable } from 'rxjs';
import { finalize, tap } from 'rxjs/operators';
import { Router } from '@angular/router';
import { SessionService } from './services/session.service';
import { LoaderService } from './services/loader.service';

@Injectable()
export class InterceptorService implements HttpInterceptor {
    constructor(private router: Router, private sessionService: SessionService, private loaderService : LoaderService) { 

    }
    intercept(httpRequest: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if(httpRequest.url.includes("GetMatchOdds")!=true){
            this.loaderService.isLoading.next(true);
        }
        return next.handle(httpRequest).pipe(
            finalize(() => {
                if(httpRequest.url.includes("SaveBets")!=true && httpRequest.url.includes("GetMatchOdds")!=true){
                    this.loaderService.isLoading.next(false);
                }
            })
        );
    }
}