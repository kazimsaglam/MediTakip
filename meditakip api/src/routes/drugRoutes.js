"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const express_1 = require("express");
const drugController_1 = require("../controllers/drugController");
const router = (0, express_1.Router)();
router.get('/list', drugController_1.drugsList);
router.get('/stock/list', drugController_1.drugStockList);
router.post('/recommend', drugController_1.getRecommendDrugs);
exports.default = router;
