import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SparePart } from './sparepart';
import { ToolType } from './toolType';

@Injectable()
export class DataService {
    private urlBrands = "api/brands";
    private urlTools = "api/tools";
    private urlSpareParts = "api/spareparts";

    constructor(private http: HttpClient) {

    }

    // toolTypes


    // spareParts
    getSpareParts() {
        return this.http.get(this.urlSpareParts);
    }

    getSparePart(id: string) {
        return this.http.get(this.urlSpareParts + '/' + id);
    }

    createSparePart(sparePart: SparePart) {
        return this.http.post(this.urlSpareParts, sparePart);
    }

    updateSparePart(sparePart: SparePart) {
        return this.http.put(this.urlSpareParts, sparePart);
    }

    deleteSparePart(id: string) {
        return this.http.delete(this.urlSpareParts + '/' + id);
    }

}
