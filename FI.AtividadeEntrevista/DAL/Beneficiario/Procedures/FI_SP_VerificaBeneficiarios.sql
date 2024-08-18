﻿CREATE PROC FI_SP_VerificaBeneficiarios
	@CPF VARCHAR(14),
	@IDCLIENTE BIGINT 

AS
BEGIN
	SELECT 1 FROM BENEFICIARIOS WHERE CPF = @CPF AND IDCLIENTE = @IDCLIENTE
END