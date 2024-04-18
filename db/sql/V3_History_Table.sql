
-- Opret History-tabel
CREATE TABLE History (
        Id INT AUTO_INCREMENT PRIMARY KEY,
        Date DATETIME NOT NULL,
        Source VARCHAR(5) NOT NULL,
        Target VARCHAR(5) NOT NULL,
        Value DECIMAL NOT NULL,
        Result DECIMAL NOT NULL,
        FOREIGN KEY (Source) REFERENCES Currency(iso),
        FOREIGN KEY (Target) REFERENCES Currency(iso)
);