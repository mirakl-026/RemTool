import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import { Brand } from './brand';
import { Tool } from './tool';
import { SparePart } from './sparepart'

@Injectable()
export class DataService {
    private urlBrands = "api/brands";
    private urlTools = "api/tools";
    private urlSpareParts = "api/spareparts";

    constructor(private http: HttpClient) {

    }


    // brands
    getBrands() {
        return this.http.get(this.urlBrands);
    }

    getBrand(id: string) {
        return this.http.get(this.urlBrands + '/' + id);
    }

    createBrand(brand: Brand) {
        return this.http.post(this.urlBrands, brand);
    }

    updateBrand(brand: Brand) {
        return this.http.put(this.urlBrands, brand);
    }

    deleteBrand(id: string) {
        return this.http.delete(this.urlBrands + '/' + id);
    }


    // tools
    getTools() {
        return this.http.get(this.urlTools);
    }

    getTool(id: string) {
        return this.http.get(this.urlTools + '/' + id);
    }

    createTool(tool: Tool) {
        return this.http.post(this.urlTools, tool);
    }

    updateTool(tool: Tool) {
        return this.http.put(this.urlTools, tool);
    }

    deleteTool(id: string) {
        return this.http.delete(this.urlTools + '/' + id);
    }


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