package main

import (
	"net/http"
	"os"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
)

func fileExists(filename string) bool {
	info, err := os.Stat(filename)
	if os.IsNotExist(err) {
		return false
	}
	return !info.IsDir()
}

func main() {
	router := gin.Default()

	config := cors.DefaultConfig()
	config.AllowOrigins = []string{"http://127.0.0.1:8002", "http://localhost:8002"}
	router.Use(cors.New(config))

	router.GET("/login", func(c *gin.Context) {
		c.JSON(http.StatusOK, gin.H{"email": "juhasz.balint.bebe@gmail.com"})
	})

	router.GET("/todo", func(c *gin.Context) {
		// create todo CRUD
		// wrap redis - mongodb cache
		c.JSON(http.StatusOK, gin.H{"email": "juhasz.balint.bebe@gmail.com"})
	})

	// go run $env:GOROOT/src/crypto/tls/generate_cert.go --host 127.0.0.1
	if fileExists("cert.pem") && fileExists("key.pem") {
		router.RunTLS(":8001", "cert.pem", "key.pem")
	} else {
		router.Run(":8001")
	}
}
